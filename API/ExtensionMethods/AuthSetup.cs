using API.CustomExceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.ExtensionMethods;

public static class AuthSetup
{
    public static IServiceCollection AddJwtConfiguration(
        this IServiceCollection services, WebApplicationBuilder builder)
    {
        services
            .AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    NameClaimType = JwtRegisteredClaimNames.Sub,
                    RoleClaimType = ClaimTypes.Role,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        Environment.GetEnvironmentVariable("F1WebAPIJwtToken",
                                                            EnvironmentVariableTarget.User) ??
                        throw new NullReferenceException("Could not get the environment variable for the jwt token.")))
                };
            });

        return services;
    }
}
