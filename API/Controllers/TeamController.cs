using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly ITeamStandingService _teamStandingService;
    private readonly IRaceResultService _raceResultService;

    public TeamsController(
        ITeamService service, 
        ITeamStandingService teamStandingService,
        IRaceResultService raceResultService)
    {
        _teamService = service;
        _teamStandingService = teamStandingService;
        _raceResultService = raceResultService;
    }

    [HttpGet]
    public async Task<ActionResult<TeamDto>> GetTeams()
    {
        IEnumerable<TeamDto> teams = await _teamService.GetTeams();

        return Ok(teams);
    }

    [HttpGet]
    [Route("{year}")]
    public async Task<ActionResult<TeamStandingDto>> GetTeamStandings(int year)
    {
        IEnumerable<TeamStandingDto> teamStandings = await 
            _teamStandingService.GetTeamStandings(year);

        return Ok(teamStandings);
    }

    [HttpGet]
    [Route("{teamId}/{year}/raceresults")]
    public async Task<ActionResult<RaceResultDto>> GetTeamRaceResultsByYear(int teamId, int year)
    {
        IEnumerable<RaceResultDto> raceResults = await
            _raceResultService.GetTeamRaceResultsByYear(teamId, year);

        return Ok(raceResults);
    }

}
