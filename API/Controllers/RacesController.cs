using API.Data.ModelViews;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class RacesController : ControllerBase
{
	private readonly RaceService _service;

	public RacesController(RaceService service)
	{
		_service = service;
	}

	[HttpGet]
	[Route("{year}")]
	public async Task<ActionResult<RaceView>> GetRaces(int year)
	{
		IEnumerable<RaceView> races = await _service.GetRaces(year);

		return Ok(races);
	}
}
