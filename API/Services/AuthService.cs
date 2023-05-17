using API.CustomExceptions;
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
    private readonly TimeSpan _accessTokenLifetime = TimeSpan.FromMinutes(1);

    private readonly SymmetricSecurityKey _securityKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(
            "F1WebAPIJwtToken", EnvironmentVariableTarget.User) ?? throw new NullReferenceException("Security key not found.")));

    private readonly F1WebAPIContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(F1WebAPIContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public IEnumerable<User> GetUsers()
    {
        return _context.Users.Include(u => u.RefreshTokens);
    }

    public async Task<RegistrationResult> Register(UserDto request)
    {
        if (_context.Users.Any(u => u.Username == request.Username))
        {
            throw new Exception("Username taken");
        }

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

    public async Task<AuthenticationResult> Login(UserDto request)
    {
        User existingUser = await _context.Users.FirstOrDefaultAsync(u =>
            u.Username == request.Username);

        if (existingUser == null)
        {
            throw new Exception("Invalid username or password");
        }

        User user = AuthenticateUser(existingUser, request.Password);

        return await GetAccessToken(existingUser);
    }

    public async Task<AuthenticationResult> Refresh(AuthenticationResult oldToken)
    {
        ClaimsPrincipal principal = GetPrincipalFromExpiredToken(oldToken.AccessToken);

        if (!int.TryParse(principal.Claims.First(c => 
            c.Type == ClaimTypes.NameIdentifier).Value, out int oldTokenUserId))
        {
            // No user 
        }

        RefreshToken oldRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt =>
            rt.UserId == oldTokenUserId && rt.Content == oldToken.RefreshToken);

        if (oldRefreshToken == null)
        {
            // invalid refresh token
        }

        User user = await _context.Users.FirstAsync(u =>
            u.UserId == oldTokenUserId);

        AuthenticationResult result = await GetAccessToken(user);

        RefreshToken newRefreshToken = new()
        {
            Content = result.RefreshToken,
            User = user,
            UserId = user.UserId
        };

        await _context.RefreshTokens.AddAsync(newRefreshToken);

        //_context.Remove(oldRefreshToken);
        await _context.SaveChangesAsync();

        return result;
    }

    private User AuthenticateUser(User user, string password)
    {
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new Exception("Invalid username or password");
        }

        return user;
    }

    private async Task<string> GetRefreshToken(string username)
    {
        User existingUser = await _context.Users.FirstOrDefaultAsync(
            u => u.Username == username);

        if (existingUser == null)
        {
            throw new Exception("User does not exist");
        }

        RefreshToken refreshToken = new()
        {
            Content = Guid.NewGuid().ToString(),
            User = existingUser,
            UserId = existingUser.UserId
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        return refreshToken.Content;
    }

    private async Task<AuthenticationResult> GetAccessToken(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username)

            //new Claim(ClaimTypes.id, Guid.NewGuid().ToString()),
            //new Claim(JwtRegisteredClaimNames.Name, request.Username)
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

        string accessToken = tokenHandler.WriteToken(token);

        return new AuthenticationResult
        {
            AccessToken = accessToken,
            RefreshToken = await GetRefreshToken(user.Username),
            Expiry = expiry
        };
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _securityKey,
            ValidateIssuer = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = _configuration["Jwt:Audience"],
            ValidateLifetime = false // Validating an expired token, so lifetime validation is disabled.
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal principal = tokenHandler.ValidateToken(
            token, tokenValidationParameters, out SecurityToken securityToken);

        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, 
            StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

}
