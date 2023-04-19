using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    private const int _maxPageSize = 40;

    private readonly ITeamService _teamService;

    public TeamsController(ITeamService service)
    {
        _teamService = service;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedTeamsDto>> GetTeams(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string name = null)
    {
        PaginatedTeamsDto teams = await _teamService.GetTeams(
            page, pageSize, _maxPageSize, name, HttpContext);

        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDto>> GetTeamById(int id)
    {
        TeamDto team = await _teamService.GetTeamById(id, HttpContext);

        return Ok(team);
    }

    [HttpGet("TeamStanding")]    
    public async Task<ActionResult<IEnumerable<TeamStandingDto>>> GetTeamStandings(
        [FromQuery] int year = 2022)
    {        
        IEnumerable<TeamStandingDto> teamStandings = await
            _teamService.GetTeamStanding(year, HttpContext);

        return Ok(teamStandings);
    }

    [HttpGet("{id}/TeamStandings")]
    public async Task<ActionResult<IEnumerable<TeamStandingDto>>> GetTeamAllStandingsById(
        int id)
    {
        IEnumerable<TeamStandingDto> teamStandings = await
            _teamService.GetTeamAllStandingsByTeamId(id, HttpContext);

        return Ok(teamStandings);
    }

    [HttpGet("{id}/RaceResults")]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetTeamRaceResultsByYear(
        int id, [FromQuery] int year = 2022)
    {
        IEnumerable<RaceResultDto> raceResults = await 
            _teamService.GetTeamRaceResultsByYear(id, year, HttpContext);

        return Ok(raceResults);
    }
}
