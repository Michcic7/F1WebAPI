using API.CustomExceptions.AuthExceptions;
using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services;

public class AuthService : IAuthService
{
    private readonly TimeSpan _accessTokenLifetime = TimeSpan.FromMinutes(10);
    private readonly TimeSpan _refreshTokenLifetime = TimeSpan.FromDays(1);

    private readonly SymmetricSecurityKey _securityKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(
            "F1WebAPIJwtToken", EnvironmentVariableTarget.Process) ?? throw new NullReferenceException("Security key not found.")));

    private readonly F1WebAPIContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(F1WebAPIContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.Include(u => u.RefreshTokens).ToListAsync();
    }

    public async Task<RegistrationResult> Register(UserDto request, HttpContext httpContext)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new EmptyUserCredentialsException(httpContext.Request.Path);
        }
        
        if (_context.Users.Any(u => u.Username == request.Username))
        {
            throw new UsernameTakenException(httpContext.Request.Path);
        }

        // Hash the inputed password.
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        User user = new()
        {
            Username = request.Username,
            PasswordHash = passwordHash
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return new RegistrationResult{ Username = request.Username };
    }

    public async Task<AuthenticationResult> Login(UserDto request, HttpContext httpContext)
    {
        // Check if the username is valid.
        User existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username) ??
            throw new InvalidUserCredentialsException(httpContext.Request.Path);

        // Check if the password is valid.
        if (!IsPasswordValid(existingUser, request.Password))
        {
            throw new InvalidUserCredentialsException(httpContext.Request.Path);
        }

        // Check if the user's refresh token has expired.
        RefreshToken? refreshToken = existingUser.RefreshTokens?.Find(rt => rt.Expiry < DateTime.UtcNow);
        if (refreshToken == null)
        {
            refreshToken = GetRefreshToken(existingUser);

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        AccessToken accessToken = GetAccessToken(existingUser);

        return new AuthenticationResult
        {
            AccessToken = accessToken.Token,
            RefreshToken = refreshToken.Content,
            Expiry = DateTime.UtcNow.Add(_accessTokenLifetime)
        };
    }

    public async Task<AccessToken> Refresh(AuthenticationResult oldTokens, HttpContext httpContext)
    {
        // Get the access token's claims.
        ClaimsPrincipal principalAccessToken = GetPrincipalFromAccessToken(oldTokens.AccessToken, httpContext);
                
        // Get the user's ID from the request body.
        if (!int.TryParse(principalAccessToken.Claims.First(c => 
            c.Type == ClaimTypes.NameIdentifier).Value, out int oldTokensUserId))
        {
            throw new InvalidAccessTokenException(httpContext.Request.Path);
        }

        // Check the user's credential.
        User? existingUser = await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.UserId == oldTokensUserId);
        if (existingUser == null)
        {
            throw new InvalidUserCredentialsException(httpContext.Request.Path);
        }

        // Check if the user's refresh token is valid.
        RefreshToken? refreshToken = existingUser.RefreshTokens?.FirstOrDefault(rt =>
            rt.Content == oldTokens.RefreshToken);
        if (refreshToken == null || refreshToken.Expiry < DateTime.UtcNow)
        {
            throw new ExpiredRefreshTokenException(httpContext.Request.Path);
        }

        // Remove the old refresh token and add a new one.
        IEnumerable<RefreshToken> userRefreshTokens = existingUser.RefreshTokens!;
        _context.RefreshTokens.RemoveRange(userRefreshTokens);
        await _context.RefreshTokens.AddAsync(GetRefreshToken(existingUser));
        await _context.SaveChangesAsync();

        // Get a new access token.
        AccessToken accessToken = GetAccessToken(existingUser);

        return accessToken;
    }

    private bool IsPasswordValid(User user, string password)
    {
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return false;
        }

        return true;
    }

    private RefreshToken GetRefreshToken(User user)
    {
        return new RefreshToken
        {
            Content = Guid.NewGuid().ToString(),
            Expiry = DateTime.UtcNow.Add(_refreshTokenLifetime),
            User = user,
            UserId = user.UserId
        };
    }

    private AccessToken GetAccessToken(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        SigningCredentials credentials = new(_securityKey, SecurityAlgorithms.HmacSha512);

        DateTime expiry = DateTime.UtcNow.Add(_accessTokenLifetime);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiry,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = credentials
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenContent = tokenHandler.WriteToken(token);

        return new AccessToken
        {
            Token = tokenContent,
            Expiry = expiry
        };
    }

    private ClaimsPrincipal GetPrincipalFromAccessToken(
        string accessTokenContent, HttpContext httpContext)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _securityKey,
            ValidateIssuer = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = _configuration["Jwt:Audience"],
            ValidateLifetime = false, // Validating an expired token, so lifetime validation is disabled.

            NameClaimType = JwtRegisteredClaimNames.Sub,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken securityToken;
        ClaimsPrincipal principal;

        try
        {
            principal = tokenHandler.ValidateToken(
                accessTokenContent, tokenValidationParameters, out securityToken);
        }
        catch (Exception)
        {
            throw new InvalidAccessTokenException(httpContext.Request.Path);
        }

        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, 
            StringComparison.InvariantCultureIgnoreCase))
        {
            throw new InvalidAccessTokenException(httpContext.Request.Path);
        }

        return principal;
    }
}
