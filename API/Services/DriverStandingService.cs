using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IDriverStandingService
{
    Task<IEnumerable<DriverStandingDto>> GetDriverStandings(int year);
}

public class DriverStandingService : IDriverStandingService
{
    private readonly F1WebAPIContext _context;

    public DriverStandingService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DriverStandingDto>> GetDriverStandings(int year)
    {
        List<DriverStanding> driverStandings = await _context.DriverStandings
            .Where(ds => ds.Year == year)
            .Include(ds => ds.Driver)
            .Include(dc => dc.Team)
            .OrderBy(ds => ds.Position)
            .ToListAsync();

        return driverStandings.Select(ds =>
        {
            return new DriverStandingDto
            {
                Position = ds.Position,
                DriverName = ds.Driver.FirstName + " " + ds.Driver.LastName,
                Nationality = ds.Driver.Nationality,
                TeamName = ds.Team.Name,
                Points = ds.Points
            };
        });
    }
}
