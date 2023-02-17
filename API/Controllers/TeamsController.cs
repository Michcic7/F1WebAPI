using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
	private readonly ITeamService _service;

	public TeamsController(ITeamService service)
	{
		_service = service;
	}

	/// <summary>
	/// Gets team standings from a specified year.
	/// </summary>
	/// <param name="year">The year must be between 1958-2022.</param>
	/// <returns>
	/// Team standings from a specified year.
	/// </returns>
	[HttpGet]
	[Route("{year}")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<TeamDto>> GetTeams(int year)
	{
		IEnumerable<TeamDto> teams = await _service.GetTeams(year);

		return Ok(teams);
	}
}
