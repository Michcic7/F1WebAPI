using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

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
        if (pageSize <= 0 || page <= 0)
        {
            return BadRequest();
        }

        IEnumerable<TeamDto> teams = await _teamService.GetTeams();        

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

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDto>> GetTeamById(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        
        TeamDto team = await _teamService.GetTeamById(id);

        if (team is null)
        {
            return NotFound();
        }

        return Ok(team);
    }

    [HttpGet("TeamStanding")]    
    public async Task<ActionResult<IEnumerable<TeamStandingDto>>> GetTeamStandings(
        [FromQuery] int year = 2022)
    {
        if (year < 1958 || year > 2022)
        {
            return BadRequest();
        }
        
        IEnumerable<TeamStandingDto> teamStandings = await
            _teamStandingService.GetTeamStanding(year);

        return Ok(teamStandings);
    }

    [HttpGet("{id}/TeamStandings")]
    public async Task<ActionResult<IEnumerable<TeamStandingDto>>> GetTeamAllStandingsById(
        int id)
    {
        IEnumerable<TeamStandingDto> teamStandings = await
            _teamStandingService.GetTeamAllStandingsById(id);

        if (teamStandings is null)
        {
            return NotFound();
        }

        return Ok(teamStandings);
    }

    [HttpGet("{id}/RaceResults")]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetTeamRaceResultsByYear(
        int id, [FromQuery] int year = 2022)
    {
        if (year < 1958 || year > 2022)
        {
            return BadRequest();
        }

        IEnumerable<RaceResultDto> raceResults = await 
            _raceResultService.GetTeamRaceResultsByYear(id, year);

        if (raceResults is null)
        {
            return NotFound();
        }

        return Ok(raceResults);
    }
}
