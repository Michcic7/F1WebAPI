using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    private const int _maxPageSize = 40;
    private const int _defaultYear = 2023;

    private readonly ITeamService _teamService;

    public TeamsController(ITeamService service)
    {
        _teamService = service;
    }

    /// <summary>
    /// Get all teams.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">How many teams to include per page.</param>
    /// <param name="name">The name to filter teams.</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedTeamsDto>> GetTeams(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string name = null)
    {
        PaginatedTeamsDto teams = await _teamService.GetTeams(
            page, pageSize, _maxPageSize, name, HttpContext);

        return Ok(teams);
    }

    /// <summary>
    /// Get a team by the ID.
    /// </summary>
    /// <param name="id">The ID of the team.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeamDto>> GetTeamById(int id)
    {
        TeamDto team = await _teamService.GetTeamById(id, HttpContext);

        return Ok(team);
    }

    /// <summary>
    /// Get the teams' championship standing from the given year.
    /// </summary>
    /// <param name="year">The year of the championship.</param>
    /// <returns></returns>
    [HttpGet("TeamStanding")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TeamStandingDto>>> GetTeamStandings(
        [FromQuery] int year = _defaultYear)
    {        
        IEnumerable<TeamStandingDto> teamStandings = await
            _teamService.GetTeamStanding(year, HttpContext);

        return Ok(teamStandings);
    }

    /// <summary>
    /// Get all championships the given team participated in.
    /// </summary>
    /// <param name="id">The ID of the team.</param>
    /// <returns></returns>
    [HttpGet("{id}/TeamStandings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TeamStandingDto>>> GetTeamAllStandingsById(
        int id)
    {
        IEnumerable<TeamStandingDto> teamStandings = await
            _teamService.GetTeamAllStandingsByTeamId(id, HttpContext);

        return Ok(teamStandings);
    }

    /// <summary>
    /// Get all race results of the given team from the given year.
    /// </summary>
    /// <param name="id">The ID of the team.</param>
    /// <param name="year">The year of races.</param>
    /// <returns></returns>
    [HttpGet("{id}/RaceResults")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetTeamRaceResultsByYear(
        int id, [FromQuery] int year = _defaultYear)
    {
        IEnumerable<RaceResultDto> raceResults = await 
            _teamService.GetTeamRaceResultsByYear(id, year, HttpContext);

        return Ok(raceResults);
    }
}
