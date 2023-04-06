using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ITeamStandingService
{
	Task<IEnumerable<TeamStandingDto>> GetTeamStanding(int year);
	Task<IEnumerable<TeamStandingDto>> GetTeamAllStandingsById(int id);
}

public class TeamStandingService : ITeamStandingService
{
    private readonly F1WebAPIContext _context;

    public TeamStandingService(F1WebAPIContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TeamStandingDto>> GetTeamStanding(int year)
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
                Points = ts.Points,
                Year = ts.Year
            };
        });
    }

    public async Task<IEnumerable<TeamStandingDto>> GetTeamAllStandingsById(int id)
    {
        List<TeamStanding> teamStandings = await _context.TeamStandings
            .Where(ts => ts.TeamId == id)
            .Include(ts => ts.Team)
            .OrderBy(ts => ts.Year)
            .ToListAsync();

        if (teamStandings.Any())
        {
            return teamStandings.Select(ts =>
            {
                return new TeamStandingDto
                {
                    Position = ts.Position,
                    Name = ts.Team.Name,
                    Points = ts.Points,
                    Year = ts.Year
                };
            });
        }
        else
        {
            return null;
        }
    }
}
