using API.Data.DTOs;
using API.Data.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private const int maxPageSize = 40;

    private readonly IDriverService _driverService;
    private readonly IDriverStandingService _driverStandingService;
    private readonly IRaceResultService _raceResultService;

    public DriversController(
        IDriverService service, 
        IDriverStandingService driverStandingService, 
        IRaceResultService raceResultService)
    {
        _driverService = service;
        _driverStandingService = driverStandingService;
        _raceResultService = raceResultService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetDrivers(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string name = null)
    {
        IEnumerable<DriverDto> drivers = await _driverService.GetDrivers();

        if (pageSize <= 0 || page <= 0)
        {
            return BadRequest();
        }

        if (!string.IsNullOrEmpty(name))
        {
            drivers = drivers.Where(d => 
                d.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase) || 
                d.Name.EndsWith(name, StringComparison.OrdinalIgnoreCase));
        }

        int totalDrivers = drivers.Count();
        int totalPages = (int)Math.Ceiling((double)totalDrivers / pageSize);

        if (page > totalPages)
        {
            return BadRequest();
        }

        IEnumerable<DriverDto> paginatedDrivers = drivers.Skip((page - 1) * pageSize).Take(pageSize);

        var response = new
        {
            TotalDrivers = totalDrivers,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            NameFilter = name,
            Drivers = paginatedDrivers
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDto>> GetDriverById(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        
        DriverDto driver = await _driverService.GetDriverById(id);

        if (driver is null)
        {
            return NotFound();
        }

        return Ok(driver);
    }

    [HttpGet("DriverStandings")]
    public async Task<ActionResult<IEnumerable<DriverStandingDto>>> GetDriverStandings(
        [FromQuery] int year = 2022)
    {
        if (year < 1950 || year > 2022)
        {
            return BadRequest();
        }
        
        IEnumerable<DriverStandingDto> driverStandings = await 
            _driverStandingService.GetDriverStandings(year);

        return Ok(driverStandings);
    }

    [HttpGet("{id}/DriverStandings")]
    public async Task<ActionResult<IEnumerable<DriverStandingDto>>> GetDriverAllStandingsById(
        int id)
    {
        IEnumerable<DriverStandingDto> driverStandings = await
            _driverStandingService.GetDriverAllStandingsById(id);

        if (driverStandings is null)
        {
            return NotFound();
        }

        return Ok(driverStandings);
    }

    [HttpGet("{id}/RaceResults")]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetDriverRaceResultsByYear(
        int id, [FromQuery] int year = 2022)
    {
        if (year < 1950 || year > 2022)
        {
            return BadRequest();
        }
        
        IEnumerable<RaceResultDto> raceResults = await
            _raceResultService.GetDriverRaceResultsByYear(id, year);

        if (raceResults is null)
        {
            return NotFound();
        }

        return Ok(raceResults);
    }
}
