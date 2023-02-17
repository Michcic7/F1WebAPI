using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class RacesController : ControllerBase
{
	private readonly IRaceService _service;

	public RacesController(IRaceService service)
	{
		_service = service;
	}

	/// <summary>
	/// Gets race standings from a specified year.
	/// </summary>
	/// <param name="year">The year must be between 1950-2022</param>
	/// <returns>
	/// Race standings from a specified year
	/// </returns>
	[HttpGet]
	[Route("{year}")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<RaceDto>> GetRaces(int year)
	{
		IEnumerable<RaceDto> races = await _service.GetRaces(year);

		return Ok(races);
	}
}
