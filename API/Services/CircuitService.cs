using API.CustomExceptions;
using API.Data;
using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using API.Data.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class CircuitService : ICircuitService
{
    private const int _startYear = 1950;
    private readonly int _endYear = DateTime.Now.Year;

    private readonly F1WebAPIContext _context;

    public CircuitService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<PaginatedCircuitsDto> GetCircuits(
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

        IQueryable<Circuit> query = _context.Circuits;

        // Apply name filter to a query.
        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            nameFilter = nameFilter.ToLower();

            query = query.Where(d =>
                d.Name.ToLower().Contains(nameFilter));
        }

        // Calculate metadata.
        int totalCircuits = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalCircuits / pageSize);
        
        // Chain the query further and iterate over it.
        IEnumerable<CircuitDto> circuits = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CircuitDto
            {
                CircuitId = c.CircuitId,
                Name = c.Name,
                Location = c.Location
            })
            .ToListAsync();

        if (page != 1 && page > totalPages)
        {
            throw new PageNumberExceededTotalPagesException(context.Request.Path);
        }

        if (!circuits.Any())
        {
            throw new FilteredEntitiesNotFoundException(typeof(Circuit), context.Request.Path);
        }

        return new PaginatedCircuitsDto
        {
            TotalCircuits = totalCircuits,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            NameFilter = nameFilter?.ToLower(),
            Circuits = circuits
        };
    }

    public async Task<CircuitDto> GetCircuitById(int id, HttpContext context)
    {
        if (id <= 0)
        {
            throw new InvalidEntityIdException(typeof(Circuit), context.Request.Path);
        }

        CircuitDto circuit = await _context.Circuits
            .Where(c => c.CircuitId == id)
            .Select(c => new CircuitDto
            {
                CircuitId = c.CircuitId,
                Name = c.Name,
                Location = c.Location
            })
            .FirstOrDefaultAsync() ??
                throw new EntityNotFoundException(typeof(Circuit), id, context.Request.Path);

        return circuit;
    }

    public async Task<IEnumerable<RaceResultDto>> GetCircuitRaceResultsByYear(
        int id, int year, HttpContext context)
    {
        if (id <= 0)
        {
            throw new InvalidEntityIdException(typeof(Circuit), context.Request.Path);
        }

        if (year < _startYear || year > _endYear)
        {
            throw new InvalidYearException(typeof(RaceResult), context.Request.Path);
        }

        bool wereRacesInThatYear = await _context.RaceResults
            .AnyAsync(rr => rr.CircuitId == id && rr.Year == year);

        if (!wereRacesInThatYear)
        {
            throw new CircuitNoRaceInThatYearException(context.Request.Path);
        }

        return await _context.RaceResults
            .Where(rr => rr.Year == year && rr.CircuitId == id)
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .OrderBy(rr => rr.Date)
            // Move DNFs to the bottom of the list
            .ThenBy(rr => rr.Position == 0 ? int.MaxValue : rr.Position)
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
