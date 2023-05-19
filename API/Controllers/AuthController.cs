using API.Data.DTOs;
using API.Data.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpGet("users")]
    public async Task<ActionResult<User>> GetUsers()
    {
        IEnumerable<User> users = _service.GetUsers();

        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(
        [FromBody] UserDto request)
    {
        RegistrationResult result = await _service.Register(request, HttpContext);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(
        [FromBody] UserDto request)
    {
        AuthenticationResult result = await _service.Login(request, HttpContext);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> Refresh(
        [FromBody] AuthenticationResult request)
    {
        AccessToken token = await _service.Refresh(request, HttpContext);

        return Ok(token);
    }
}
