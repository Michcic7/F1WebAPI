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
        IEnumerable<User> users = await _service.GetUsers();

        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(
        [FromBody] UserDto request)
    {
        RegistrationResult result = await _service.Register(request);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(
        [FromBody] UserDto request)
    {
        AuthenticationResult result = await _service.Login(request);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> Refresh(
        [FromBody] AuthenticationResult request)
    {
        AuthenticationResult result = await _service.Refresh(request);

        return Ok(result);
    }

    //[HttpPost("token")]
    //public async Task<ActionResult<string>> GenerateJwtToken(
    //    [FromBody] UserDto request)
    //{
    //    var token = _service.GenerateJwtToken(request);

    //    return Ok(token);
    //}
}
