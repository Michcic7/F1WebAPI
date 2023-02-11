using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OldDriverController : ControllerBase
{
	private readonly DriverService _service;
	
	public OldDriverController(DriverService service)
	{
		_service = service;
	}
	
	[HttpGet]
	[ProducesResponseType(200, Type = typeof(Driver))]
	[ProducesResponseType(400)]
	[Route("{year}")]
	public ActionResult<Driver> GetDrivers(int year)
	{
		if (year < 1950 || year > DateTime.Now.Year)
		{
			return BadRequest($"Year must be between 1950 and {DateTime.Now.Year}");
		}

		var drivers = _service.GetDriverStandings(year);

		return Ok(drivers);
	}
}
