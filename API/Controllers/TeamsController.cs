using API.Data.ModelViews;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
	private readonly TeamService _service;

	public TeamsController(TeamService service)
	{
		_service = service;
	}

	[HttpGet]
	[Route("{year}")]
	public async Task<ActionResult<TeamView>> GetTeams(int year)
	{
		IEnumerable<TeamView> teams = await _service.GetTeams(year);

		return Ok(teams);
	}
}
