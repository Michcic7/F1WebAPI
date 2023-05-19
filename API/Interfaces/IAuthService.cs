using API.Data.DTOs;
using API.Data.Models;

namespace API.Interfaces;

public interface IAuthService
{
    IEnumerable<User> GetUsers();
    Task<RegistrationResult> Register(UserDto request, HttpContext httpContext);
    Task<AuthenticationResult> Login(UserDto request, HttpContext httpContext);
    Task<AccessToken> Refresh(AuthenticationResult oldToken, HttpContext httpContext);
}