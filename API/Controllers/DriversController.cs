using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
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
    public async Task<ActionResult<DriverDto>> GetDrivers()
    {
        IEnumerable<DriverDto> drivers = await _driverService.GetDrivers();

        return Ok(drivers);
    }

    [HttpGet]
    [Route("{year}")]
    public async Task<ActionResult<DriverStandingDto>> GetDriverStandings(int year)
    {
        IEnumerable<DriverStandingDto> driverStandings = await 
            _driverStandingService.GetDriverStandings(year);

        return Ok(driverStandings);
    }

    [HttpGet]
    [Route("{driverId}/{year}/raceresults")]
    public async Task<ActionResult<RaceResultDto>> GetRaceResultsByYear(int driverId, int year)
    {
        IEnumerable<RaceResultDto> raceResults = await
            _raceResultService.GetDriverRaceResultsByYear(driverId, year);

        return Ok(raceResults);
    }
}
