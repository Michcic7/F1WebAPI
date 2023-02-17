using API.Data;
using API.Data.Models;
using API.Data.DTOs;
using API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ITeamService
{
	Task<IEnumerable<TeamDto>> GetTeams(int year);
}

public class TeamService : ITeamService
{
	private readonly F1WebAPIContext _context;

	public TeamService(F1WebAPIContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<TeamDto>> GetTeams(int year)
	{
		if (year < 1958 || year > 2022)
		{
			throw new ArgumentException("The year must be between 1958 and 2022");
		}

		List<Team> teams = await
			_context.Teams.Where(t => t.StandingsYear.StandingsYearId == year).ToListAsync();

		if (teams.Count is 0)
		{
			throw new ArgumentNullException();
		}

		return teams.Select(t =>
		{
			return new TeamDto
			{
				Position = t.Position,
				Name = t.Name,
				Points = t.Points
			};
		});
	}
}
