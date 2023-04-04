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

    public DriversController(
        IDriverService service, 
        IDriverStandingService driverStandingService)
    {
        _driverService = service;
        _driverStandingService = driverStandingService;

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
        IEnumerable<DriverStandingDto> driverStandings = await _driverStandingService.GetDriverStandings(year);

        return Ok(driverStandings);
    }
}
