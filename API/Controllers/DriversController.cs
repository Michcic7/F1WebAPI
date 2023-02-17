using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController :ControllerBase
{
	private readonly IDriverService _service;

	public DriversController(IDriverService service)
	{
		_service = service;
	}
	
	/// <summary>
	/// Gets driver standings from a specified year.
	/// </summary>
	/// <param name="year">The year must be between 1950-2022.</param>
	/// <returns>
	/// Driver standings from a specified year.
	/// </returns>
	[HttpGet]
	[Route("{year}")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<DriverDto>> GetDrivers(int year)
	{
		IEnumerable<DriverDto> drivers = await _service.GetDrivers(year);

		return Ok(drivers);
	}

	/// <summary>
	/// Gets career results of a specified driver
	/// </summary>
	/// <param name="name">The full name of a driver without spaces</param>
	/// <returns>
	/// Returns career results about a specified driver
	/// </returns>
	[HttpGet]
	[Route("career/{name}")]
	public async Task<ActionResult<DriverCareerDto>> GetDriverByName(string name)
	{
		DriverCareerDto driver = await _service.GetDriverByName(name);

		return Ok(driver);
	}
}
