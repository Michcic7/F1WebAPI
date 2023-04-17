using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private const int _maxPageSize = 40;

    private readonly IDriverService _driverService;

    public DriversController(IDriverService service)
    {
        _driverService = service;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedDriversDto>> GetDrivers(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string name = null)
    {
        PaginatedDriversDto drivers = await _driverService
            .GetDrivers(page, pageSize, _maxPageSize, name, HttpContext);

        return Ok(drivers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDto>> GetDriverById(int id)
    {
        DriverDto driver = await _driverService.GetDriverById(id, HttpContext);

        return Ok(driver);
    }

    [HttpGet("DriverStanding")]
    public async Task<ActionResult<IEnumerable<DriverStandingDto>>> GetDriverStanding(
        [FromQuery] int year = 2022)
    {        
        IEnumerable<DriverStandingDto> driverStandings = await 
            _driverService.GetDriverStanding(year, HttpContext);

        return Ok(driverStandings);
    }

    [HttpGet("{id}/DriverStandings")]
    public async Task<ActionResult<IEnumerable<DriverStandingDto>>> GetDriverAllStandingsByDriverId(
        int id)
    {
        IEnumerable<DriverStandingDto> driverStandings = await
            _driverService.GetDriverAllStandingsByDriverId(id, HttpContext);

        return Ok(driverStandings);
    }

    [HttpGet("{id}/RaceResults")]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetDriverRaceResultsByYear(
        int id, [FromQuery] int year = 2022)
    {        
        IEnumerable<RaceResultDto> raceResults = await
            _driverService.GetDriverRaceResultsByYear(id, year, HttpContext);

        return Ok(raceResults);
    }
}
