using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    private const int maxPageSize = 40;
    
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
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string name = null)
    {
        IEnumerable<TeamDto> teams = await _teamService.GetTeams();

        if (pageSize <= 0 || page <= 0)
        {
            return BadRequest();
        }

        if (!string.IsNullOrEmpty(name))
        {
            teams = teams.Where(t => 
                t.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase) ||
                t.Name.EndsWith(name, StringComparison.OrdinalIgnoreCase));
        }

        int totalTeams = teams.Count();
        int totalPages = (int)Math.Ceiling((double)totalTeams / pageSize);

        if (page > totalPages)
        {
            return BadRequest();
        }

        IEnumerable<TeamDto> paginatedTeams = teams.Skip((page - 1) * pageSize).Take(pageSize);

        var response = new
        {
            TotalTeams = totalTeams,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            NameFilter = name,
            Drivers = paginatedTeams
        };

        return Ok(response);
    }

    [HttpGet]
    [Route("{year}")]
    public async Task<ActionResult<IEnumerable<TeamStandingDto>>> GetTeamStandings(int year)
    {
        IEnumerable<TeamStandingDto> teamStandings = await 
            _teamStandingService.GetTeamStandings(year);

        return Ok(teamStandings);
    }

    [HttpGet]
    [Route("{teamId}/{year}/raceresults")]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetTeamRaceResultsByYear(int teamId, int year)
    {
        IEnumerable<RaceResultDto> raceResults = await
            _raceResultService.GetTeamRaceResultsByYear(teamId, year);

        return Ok(raceResults);
    }

}
