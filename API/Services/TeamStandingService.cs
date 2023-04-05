using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ITeamStandingService
{
	Task<IEnumerable<TeamStandingDto>> GetTeamStandings(int year);
}

public class TeamStandingService : ITeamStandingService
{
    private readonly F1WebAPIContext _context;

    public TeamStandingService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TeamStandingDto>> GetTeamStandings(int year)
    {
        List<TeamStanding> teamStandings = await _context.TeamStandings
            .Where(ts => ts.Year == year)
            .Include(ts => ts.Team)
            .OrderBy(ts => ts.Position)
            .ToListAsync();

        return teamStandings.Select(ts =>
        {
            return new TeamStandingDto
            {
                Position = ts.Position,
                Name = ts.Team.Name,
                Points = ts.Points
            };
        });
    }
}
