using API.CustomExceptions;
using API.Data;
using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using API.Data.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace API.Services;

public class DriverService : IDriverService
{
    private readonly F1WebAPIContext _context;

    public DriverService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<PaginatedDriversDto> GetDrivers(
        int page, int pageSize, int maxPageSize, 
        string nameFilter, string nationalityFilter, HttpContext context)
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
        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            nameFilter = nameFilter.ToLower();

            query = query.Where(d =>
                d.FirstName.ToLower().Contains(nameFilter) ||
                d.LastName.ToLower().Contains(nameFilter));
        }

        // Apply nationality filter to a query.
        if (!string.IsNullOrWhiteSpace(nationalityFilter))
        {
            nationalityFilter = nationalityFilter.ToLower();

            query = query.Where(d =>
                d.Nationality.ToLower().Contains(nationalityFilter));
        }

        // Calculate metadata.
        int totalDrivers = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalDrivers / pageSize);

        // Chain the query further and iterate over it.
        IEnumerable<DriverDto> drivers = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new DriverDto
            {
                DriverId = d.DriverId,
                Name = d.FirstName + " " + d.LastName,
                Nationality = d.Nationality
            })
            .ToListAsync();

        if (page != 1 && page > totalPages)
        {
            throw new PageNumberExceededTotalPagesException(context.Request.Path);
        }

        if (drivers.Count() == 0)
        {
            throw new FilteredEntitiesNotFoundException(typeof(Driver), context.Request.Path);
        }

        return new PaginatedDriversDto
        {
            TotalDrivers = totalDrivers,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            NameFilter = nameFilter?.ToLower(),
            Drivers = drivers
        };
    }

    public async Task<DriverDto> GetDriverById(int id, HttpContext context)
    {
        if (id <= 0)
        {
            throw new InvalidEntityIdException(typeof(Driver), context.Request.Path);
        }

        DriverDto driver = await _context.Drivers
            .Where(d => d.DriverId == id)
            .Select(d => new DriverDto
            {
                DriverId = d.DriverId,
                Name = d.FirstName + " " + d.LastName,
                Nationality = d.Nationality
            })
            .FirstOrDefaultAsync();

        if (driver == null)
        {
            throw new EntityNotFoundException(typeof(Driver), id, context.Request.Path);
        }

        return driver;
    }

    public async Task<IEnumerable<DriverStandingDto>> GetDriverStanding(
        int year, HttpContext context)
    {
        if (year < 1950 || year > 2022)
        {
            throw new InvalidYearException(typeof(DriverStanding), context.Request.Path);
        }

        return await _context.DriverStandings
            .Where(ds => ds.Year == year)
            .Include(ds => ds.Driver)
            .Include(ds => ds.Team)
            // Move DNFs to the bottom of the list
            .OrderBy(ds => ds.Position == 0 ? int.MaxValue : ds.Position)
            .Select(ds => new DriverStandingDto
            {
                Position = ds.Position,
                DriverName = ds.Driver.FirstName + " " + ds.Driver.LastName,
                Nationality = ds.Driver.Nationality,
                TeamName = ds.Team.Name,
                Points = ds.Points,
                Year = ds.Year
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<DriverStandingDto>> GetDriverAllStandingsByDriverId(
        int id, HttpContext context)
    {
        if (id <= 0)
        {
            throw new InvalidEntityIdException(typeof(Driver), context.Request.Path);
        }

        bool hasDriverStandings = await _context.DriverStandings
            .AnyAsync(ds => ds.DriverId == id);

        if (!hasDriverStandings)
        {
            throw new DriverWithoutStandingsException(context.Request.Path);
        }

        return await _context.DriverStandings
            .Where(ds => ds.DriverId == id)
            .Include(ds => ds.Driver)
            .Include(ds => ds.Team)
            .OrderBy(ds => ds.Year)
            .Select(ds => new DriverStandingDto
            {
                Position = ds.Position,
                DriverName = ds.Driver.FirstName + " " + ds.Driver.LastName,
                Nationality = ds.Driver.Nationality,
                TeamName = ds.Team.Name,
                Points = ds.Points,
                Year = ds.Year
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<RaceResultDto>> GetDriverRaceResultsByYear(
        int id, int year, HttpContext context)
    {
        if (id <= 0)
        {
            throw new InvalidEntityIdException(typeof(Driver), context.Request.Path);
        }

        if (year < 1950 || year > 2022)
        {
            throw new InvalidYearException(typeof(RaceResult), context.Request.Path);
        }

        bool didRaceInThatYear = await _context.RaceResults
            .AnyAsync(rr => rr.DriverId == id && rr.Year == year);

        if (!didRaceInThatYear)
        {
            throw new DriverDidNotRaceInThatYearException(context.Request.Path);
        }

        return await _context.RaceResults
            .Where(rr => rr.Year == year && rr.DriverId == id)
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .OrderBy(rr => rr.Date)
            .Select(rr => new RaceResultDto
            {
                Position = rr.Position,
                CircuitName = rr.Circuit.Name,
                Date = rr.Date,
                DriverName = rr.Driver.FirstName + " " + rr.Driver.LastName,
                TeamName = rr.Team.Name,
                Points = rr.Points,
                Time = rr.Time,
                Laps = rr.Laps
            })
            .ToListAsync();
    }
}
