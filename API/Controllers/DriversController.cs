using API.Data.Models;
using API.Data.ModelViews;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController :ControllerBase
{
	private readonly DriverService _service;

	public DriversController(DriverService service)
	{
		_service = service;
	}
	
	[HttpGet]
	[Route("{year}")]
	public async Task<ActionResult<DriverView>> GetDrivers(int year)
	{
		IEnumerable<DriverView> drivers = await _service.GetDrivers(year);

		return Ok(drivers);
	}

	[HttpGet]
	[Route("career/{name}")]
	public async Task<ActionResult<DriverCareerView>> GetDriverByName(string name)
	{
		DriverCareerView driver = await _service.GetDriverByName(name);

		return Ok(driver);
	}
}
