using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IRaceResultService
{
    Task<IEnumerable<RaceResultDto>> GetDriverRaceResultsByYear(int id, int year);
    Task<IEnumerable<RaceResultDto>> GetTeamRaceResultsByYear(int id, int year);
    Task<IEnumerable<RaceResultDto>> GetCircuitRaceResultsByYear(int id, int year);
}

public class RaceResultService : IRaceResultService
{
    private readonly F1WebAPIContext _context;

    public RaceResultService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RaceResultDto>> GetDriverRaceResultsByYear(int id, int year)
    {
        List<RaceResult> raceResults = await _context.RaceResults
            .Where(rr => rr.Year == year && rr.DriverId == id)
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .OrderBy(rr => rr.Date)
            .ToListAsync();
        
        if (raceResults.Any())
        {
            return raceResults.Select(rr =>
            {
                return new RaceResultDto
                {
                    Position = rr.Position,
                    CircuitName = rr.Circuit.Name,
                    Date = rr.Date,
                    DriverName = rr.Driver.FirstName + " " + rr.Driver.LastName,
                    TeamName = rr.Team.Name,
                    Points = rr.Points,
                    Time = rr.Time,
                    Laps = rr.Laps
                };
            });
        }
        else
        {
            return null;
        }
    }

    public async Task<IEnumerable<RaceResultDto>> GetTeamRaceResultsByYear(int id, int year)
    {
        List<RaceResult> raceResults = await _context.RaceResults
            .Where(rr => rr.Year == year && rr.TeamId == id)
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .OrderBy(rr => rr.Date)
            .ThenBy(rr => rr.Position == 0 ? int.MaxValue : rr.Position)
            .ToListAsync();

        if (raceResults.Any())
        {
            return raceResults.Select(rr =>
            {
                return new RaceResultDto
                {
                    Position = rr.Position,
                    CircuitName = rr.Circuit.Name,
                    Date = rr.Date,
                    DriverName = rr.Driver.FirstName + " " + rr.Driver.LastName,
                    TeamName = rr.Team.Name,
                    Points = rr.Points,
                    Time = rr.Time,
                    Laps = rr.Laps
                };
            });
        }
        else
        {
            return null;
        }
    }

    public async Task<IEnumerable<RaceResultDto>> GetCircuitRaceResultsByYear(int id, int year)
    {
        List<RaceResult> raceResults = await _context.RaceResults
            .Where(rr => rr.Year == year && rr.CircuitId == id)
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .OrderBy(rr => rr.Date)
            .ThenBy(rr => rr.Position == 0 ? int.MaxValue : rr.Position)
            .ToListAsync();

        if (raceResults.Any())
        {
            return raceResults.Select(rr =>
            {
                return new RaceResultDto
                {
                    Position = rr.Position,
                    CircuitName = rr.Circuit.Name,
                    Date = rr.Date,
                    DriverName = rr.Driver.FirstName + " " + rr.Driver.LastName,
                    TeamName = rr.Team.Name,
                    Points = rr.Points,
                    Time = rr.Time,
                    Laps = rr.Laps
                };
            });
        }
        else
        {
            return null;
        }
    }
}
