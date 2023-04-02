using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private readonly IDriverService _service;

    public DriversController(IDriverService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<DriverDto>> GetDrivers()
    {
        IEnumerable<DriverDto> drivers = await _service.GetDrivers();

        return Ok(drivers);
    }
}
