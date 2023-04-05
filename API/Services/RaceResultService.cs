using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IRaceResultService
{
    Task<IEnumerable<RaceResultDto>> GetDriverRaceResultsByYear(int driverId, int year);
    Task<IEnumerable<RaceResultDto>> GetTeamRaceResultsByYear(int teamId, int year);
    Task<IEnumerable<RaceResultDto>> GetCircuitRaceResultsByYear(int teamId, int year);
}

public class RaceResultService : IRaceResultService
{
    private readonly F1WebAPIContext _context;

    public RaceResultService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RaceResultDto>> GetDriverRaceResultsByYear(int driverId, int year)
    {
        List<RaceResult> raceResults = await _context.RaceResults
            .Where(rr => rr.Year == year && rr.DriverId == driverId)
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .OrderBy(rr => rr.Date)
            .ToListAsync();
        
        return raceResults.Select(rr =>
        {
            return new RaceResultDto
            {
                Position = rr.Position,
                CircuitName = rr.Circuit.Name,
                Date = rr.Date,
                DriverName = rr.Driver.FirstName + " " + rr.Driver.LastName,
                TeamName = rr.Team.Name,
                Time = rr.Time,
                Laps = rr.Laps
            };
        });
    }

    public async Task<IEnumerable<RaceResultDto>> GetTeamRaceResultsByYear(int teamId, int year)
    {
        List<RaceResult> raceResults = await _context.RaceResults
            .Where(rr => rr.Year == year && rr.TeamId == teamId)
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .OrderBy(rr => rr.Date)
            .ThenBy(rr => rr.Position == 0 ? int.MaxValue : rr.Position)
            .ToListAsync();

        return raceResults.Select(rr =>
        {
            return new RaceResultDto
            {
                Position = rr.Position,
                CircuitName = rr.Circuit.Name,
                Date = rr.Date,
                DriverName = rr.Driver.FirstName + " " + rr.Driver.LastName,
                TeamName = rr.Team.Name,
                Time = rr.Time,
                Laps = rr.Laps
            };
        });
    }

    public async Task<IEnumerable<RaceResultDto>> GetCircuitRaceResultsByYear(int circuitId, int year)
    {
        List<RaceResult> raceResults = await _context.RaceResults
            .Where(rr => rr.Year == year && rr.CircuitId == circuitId)
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .OrderBy(rr => rr.Date)
            .ThenBy(rr => rr.Position == 0 ? int.MaxValue : rr.Position)
            .ToListAsync();

        return raceResults.Select(rr =>
        {
            return new RaceResultDto
            {
                Position = rr.Position,
                CircuitName = rr.Circuit.Name,
                Date = rr.Date,
                DriverName = rr.Driver.FirstName + " " + rr.Driver.LastName,
                TeamName = rr.Team.Name,
                Time = rr.Time,
                Laps = rr.Laps
            };
        });
    }
}
