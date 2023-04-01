using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _service;
    public TeamController(ITeamService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<TeamDto>> GetTeams()
    {
        IEnumerable<TeamDto> teams = await _service.GetTeams();

        return Ok(teams);
    }
}
