using API.Data.DTOs;
using API.Data.Models;

namespace API.Interfaces;

public interface IAuthService
{
    IEnumerable<User> GetUsers();
    Task<AuthenticationResult> Login(UserDto request);
    Task<RegistrationResult> Register(UserDto request);
    Task<AuthenticationResult> Refresh(AuthenticationResult oldToken);
}