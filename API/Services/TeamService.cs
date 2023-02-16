using API.Data;
using API.Data.Models;
using API.Data.ModelViews;
using API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class TeamService
{
	private readonly F1WebAPIContext _context;

	public TeamService(F1WebAPIContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<TeamView>> GetTeams(int year)
	{
		List<Team> teams = new();

		try
		{
			teams = await
			_context.Teams.Where(t => t.StandingsYear.StandingsYearId == year).ToListAsync();
		}
		catch (ArgumentNullException)
		{
			throw new InvalidYearException();
		}		

		Queue<TeamView> teamViews = new();

		foreach (var team in teams)
		{
			TeamView teamView = new(
				team.Position,
				team.Name,
				team.Points);

			teamViews.Enqueue(teamView);
		}

		return teamViews;
	}
}
