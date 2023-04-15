using API.CustomExceptions;
using API.Data;
using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IDriverService
{
    Task<PaginatedDriversDto> GetDrivers(
        int page, int pageSize, int maxPageSize, string nameFilter, HttpContext context);    
    Task<DriverDto> GetDriverById(int id, HttpContext context);
}

public class DriverService : IDriverService
{
    private readonly F1WebAPIContext _context;

    public DriverService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<PaginatedDriversDto> GetDrivers(
        int page, int pageSize, int maxPageSize, string nameFilter, HttpContext context)
    {
        if (page <= 0)
        {
            throw new NonPositivePageNumberException(context.Request.Path);
        }

        if (pageSize > maxPageSize)
        {
            pageSize = maxPageSize;
        }
        
        IQueryable<Driver> query = _context.Drivers;

        // Apply name filter to a query.
        if (!string.IsNullOrEmpty(nameFilter))
        {
            query = query.Where(d =>
                d.FirstName.ToLower().Contains(nameFilter.ToLower()) ||
                d.LastName.ToLower().Contains(nameFilter.ToLower()));
        }

        // Chain the query further and iterate over it.
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

        // Calculate metadata.
        int totalDrivers = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalDrivers / pageSize);

        if (page > totalPages)
        {
            throw new PageNumberExceededTotalPagesException(context.Request.Path);
        }

        PaginatedDriversDto driversWithMetadata = new()
        {
            TotalDrivers = totalDrivers,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            NameFilter = nameFilter?.ToLower(),
            Drivers = drivers
        };

        return driversWithMetadata;
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
