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
            .OrderBy(ds => ds.Position)
            .ToListAsync();

        return driverStandings.Select(ds =>
        {
            return new DriverStandingDto
            {
                //DriverStandingId = ds.DriverStandingId,
                //Year = ds.Year,
                Position = ds.Position,
                Driver = ds.Driver,
                Points = ds.Points
            };
        });
    }
}
