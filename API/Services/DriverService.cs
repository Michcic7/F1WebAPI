using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IDriverService
{
    Task<IEnumerable<DriverDto>> GetDrivers();    
    Task<DriverDto> GetDriverById(int id);
}

public class DriverService : IDriverService
{
    private readonly F1WebAPIContext _context;

    public DriverService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DriverDto>> GetDrivers()
    {
        List<Driver> drivers = await _context.Drivers.Select(d => d).ToListAsync();

        return drivers.Select(d =>
        {
            return new DriverDto
            {
                DriverId = d.DriverId,
                Name = d.FirstName + " " + d.LastName,
                Nationality = d.Nationality
            };
        });
    }

    public async Task<DriverDto> GetDriverById(int id)
    {
        Driver driver = await _context.Drivers.FindAsync(id);

        if (driver != null)
        {
            return new DriverDto
            {
                DriverId = driver.DriverId,
                Name = driver.FirstName + " " + driver.LastName,
                Nationality = driver.Nationality
            };
        }
        else
        {
            return null;
        }
    }
}
