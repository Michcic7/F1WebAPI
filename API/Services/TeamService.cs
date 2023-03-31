using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ITeamService
{
    Task<IEnumerable<TeamDto>> GetTeams();
}

public class TeamService : ITeamService
{
    private readonly F1WebAPIContext _context;

    public TeamService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TeamDto>> GetTeams()
    {
        List<Team> teams = await _context.Teams.Select(t => t).ToListAsync();

        return teams.Select(t =>
        {
            return new TeamDto
            {
                TeamId = t.TeamId,
                Name = t.Name
            };
        });
    }
}
