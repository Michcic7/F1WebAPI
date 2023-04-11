using API.CustomExceptions;
using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IDriverService
{
    Task<IEnumerable<DriverDto>> GetDrivers(
        int page, int pageSize, int maxPageSize, string nameFilter);    
    Task<DriverDto> GetDriverById(int id, HttpContext context);
}

public class DriverService : IDriverService
{
    private readonly F1WebAPIContext _context;

    public DriverService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DriverDto>> GetDrivers(
        int page, int pageSize, int maxPageSize, string nameFilter)
    {
        if (page <= 0)
        {
            throw new NotImplementedException();
        }

        if (pageSize > maxPageSize)
        {
            pageSize = maxPageSize;
        }
        
        IQueryable<Driver> query = _context.Drivers;

        if (!string.IsNullOrEmpty(nameFilter))
        {
            query = query.Where(d => 
                d.FirstName.StartsWith(nameFilter, StringComparison.OrdinalIgnoreCase) || 
                d.LastName.StartsWith(nameFilter, StringComparison.OrdinalIgnoreCase));
        }

        List<DriverDto> drivers = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new DriverDto
            {
                DriverId = d.DriverId,
                Name = d.FirstName + " " + d.LastName,
                Nationality = d.Nationality
            })
            .ToListAsync();

        return drivers;
    }

    public async Task<DriverDto> GetDriverById(int id, HttpContext context)
    {
        if (id <= 0)
        {
            throw new InvalidDriverIdException(context.Request.Path);
        }
        
        Driver driver = await _context.Drivers.FindAsync(id);

        if (driver == null)
        {
            throw new DriverNotFoundException(context.Request.Path, id);
        }

        return new DriverDto
        {
            DriverId = driver.DriverId,
            Name = driver.FirstName + " " + driver.LastName,
            Nationality = driver.Nationality
        };
    }

    //public async Task<DriverDto> GetDriverById(int id)
    //{
    //    Driver driver = await _context.Drivers.FindAsync(id);

    //    if (driver != null)
    //    {
    //        return new DriverDto
    //        {
    //            DriverId = driver.DriverId,
    //            Name = driver.FirstName + " " + driver.LastName,
    //            Nationality = driver.Nationality
    //        };
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
}
