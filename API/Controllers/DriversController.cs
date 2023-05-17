using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private const int _maxPageSize = 40;
    private const int _defaultYear = 2023;

    private readonly IDriverService _driverService;

    public DriversController(IDriverService service)
    {
        _driverService = service;
    }

    /// <summary>
    /// Get all drivers.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">How many drivers to include per page.</param>
    /// <param name="name">The name or surname to filter drivers.</param>
    /// <param name="nationality">The three-letter code to filter drivers.</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedDriversDto>> GetDrivers(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20,
        [FromQuery] string name = null, [FromQuery] string nationality = null)
    {
        PaginatedDriversDto drivers = await _driverService
            .GetDrivers(page, pageSize, _maxPageSize, name, nationality, HttpContext);

        return Ok(drivers);
    }

    /// <summary>
    /// Get a driver by the ID.
    /// </summary>
    /// <param name="id">The ID of the driver.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DriverDto>> GetDriverById(int id)
    {
        DriverDto driver = await _driverService.GetDriverById(id, HttpContext);

        return Ok(driver);
    }

    /// <summary>
    /// Get the drivers' championship standing from the given year.
    /// </summary>
    /// <param name="year">The year of the championship.</param>
    /// <returns></returns>
    [HttpGet("DriverStanding")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DriverStandingDto>>> GetDriverStanding(
        [FromQuery] int year = _defaultYear)
    {        
        IEnumerable<DriverStandingDto> driverStandings = await 
            _driverService.GetDriverStanding(year, HttpContext);

        return Ok(driverStandings);
    }

    /// <summary>
    /// Get all championships the given driver participated in.
    /// </summary>
    /// <param name="id">The ID of the driver.</param>
    /// <returns></returns>
    [HttpGet("{id}/DriverStandings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DriverStandingDto>>> GetDriverAllStandingsByDriverId(
        int id)
    {
        IEnumerable<DriverStandingDto> driverStandings = await
            _driverService.GetDriverAllStandingsByDriverId(id, HttpContext);

        return Ok(driverStandings);
    }

    /// <summary>
    /// Get all race results of the given driver from the given year.
    /// </summary>
    /// <param name="id">The ID of the driver.</param>
    /// <param name="year">The year of races.</param>
    /// <returns></returns>
    [HttpGet("{id}/RaceResults")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetDriverRaceResultsByYear(
        int id, [FromQuery] int year = _defaultYear)
    {        
        IEnumerable<RaceResultDto> raceResults = await
            _driverService.GetDriverRaceResultsByYear(id, year, HttpContext);

        return Ok(raceResults);
    }
}
