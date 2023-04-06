using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ITeamService
{
    Task<IEnumerable<TeamDto>> GetTeams();
    Task<TeamDto> GetTeamById(int id);
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

    public async Task<TeamDto> GetTeamById(int id)
    {
        Team team = await _context.Teams.FindAsync(id);

        if (team != null)
        {
            return new TeamDto
            {
                TeamId = team.TeamId,
                Name = team.Name
            };
        }
        else
        {
            return null;
        }
    }
}
