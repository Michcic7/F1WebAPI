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

    /// <summary>
    /// Get all registered user. Only for development purposes.
    /// </summary>
    /// <returns></returns>
    [HttpGet("users")]
    public async Task<ActionResult<User>> GetUsers()
    {
        IEnumerable<User> users = await _service.GetUsersAsync();

        return Ok(users);
    }

    /// <summary>
    /// Register a user.
    /// </summary>
    /// <param name="request">Json containing a username and password.</param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(
        [FromBody] UserDto request)
    {
        RegistrationResult result = await _service.Register(request, HttpContext);

        return Created("", result);
    }

    /// <summary>
    /// Login a user.
    /// </summary>
    /// <param name="request">Json containing a username and password.</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(
        [FromBody] UserDto request)
    {
        AuthenticationResult result = await _service.Login(request, HttpContext);

        return Ok(result);
    }

    /// <summary>
    /// Refresh an access token.
    /// </summary>
    /// <param name="request">The body received in the /login endpoint.</param>
    /// <returns></returns>
    [HttpPost("refresh")]
    public async Task<ActionResult> Refresh(
        [FromBody] AuthenticationResult request)
    {
        AccessToken token = await _service.Refresh(request, HttpContext);

        return Ok(token);
    }
}
