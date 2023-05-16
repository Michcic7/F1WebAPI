using API.CustomExceptions;
using API.Data;
using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using API.Data.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class TeamService : ITeamService
{
    private readonly F1WebAPIContext _context;

    public TeamService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<PaginatedTeamsDto> GetTeams(
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

        IQueryable<Team> query = _context.Teams;

        // Apply name filter to a query.
        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            nameFilter = nameFilter.ToLower();

            query = query.Where(d => d.Name.ToLower().Contains(nameFilter));
        }

        // Calculate metadata.
        int totalTeams = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalTeams / pageSize);
        
        // Chain the query further and iterate over it.
        IEnumerable<TeamDto> teams = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TeamDto
            {
                TeamId = t.TeamId,
                Name = t.Name
            })
            .ToListAsync();

        if (page != 1 && page > totalPages)
        {
            throw new PageNumberExceededTotalPagesException(context.Request.Path);
        }

        if (teams.Count() == 0)
        {
            throw new FilteredEntitiesNotFoundException(typeof(Team), context.Request.Path);
        }

        return new PaginatedTeamsDto
        {
            TotalTeams = totalTeams,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            NameFilter = nameFilter,
            Teams = teams
        };
    }

    public async Task<TeamDto> GetTeamById(int id, HttpContext context)
    {
        if (id <= 0)
        {
            throw new InvalidEntityIdException(typeof(Team), context.Request.Path);
        }

        TeamDto team = await _context.Teams
            .Where(t => t.TeamId == id)
            .Select(t => new TeamDto
            {
                TeamId = t.TeamId,
                Name = t.Name
            })
            .FirstOrDefaultAsync();

        if (team == null)
        {
            throw new EntityNotFoundException(typeof(Team), id, context.Request.Path);
        }

        return team;
    }

    public async Task<IEnumerable<TeamStandingDto>> GetTeamStanding(
        int year, HttpContext context)
    {
        if (year < 1950 || year > 2022)
        {
            throw new InvalidYearException(typeof(TeamStanding), context.Request.Path);
        }

        return await _context.TeamStandings
            .Where(ts => ts.Year == year)
            .Include(ts => ts.Team)
            .OrderBy(ts => ts.Position)
            .Select(ts => new TeamStandingDto()
            {
                Position = ts.Position,
                Name = ts.Team.Name,
                Points = ts.Points,
                Year = ts.Year
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<TeamStandingDto>> GetTeamAllStandingsByTeamId(
        int id, HttpContext context)
    {
        if (id <= 0)
        {
            throw new InvalidEntityIdException(typeof(Team), context.Request.Path);
        }

        bool hasTeamStandings = await _context.TeamStandings
            .AnyAsync(ds => ds.TeamId == id);

        if (!hasTeamStandings)
        {
            throw new TeamWithoutStandingsException(context.Request.Path);
        }

        return await _context.TeamStandings
            .Where(ts => ts.TeamId == id)
            .Include(ts => ts.Team)
            .OrderBy(ts => ts.Year)
            .Select(ts => new TeamStandingDto
            {
                Position = ts.Position,
                Name = ts.Team.Name,
                Points = ts.Points,
                Year = ts.Year
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<RaceResultDto>> GetTeamRaceResultsByYear(
        int id, int year, HttpContext context)
    {
        if (id <= 0)
        {
            throw new InvalidEntityIdException(typeof(Team), context.Request.Path);
        }

        if (year < 1950 || year > 2022)
        {
            throw new InvalidYearException(typeof(RaceResult), context.Request.Path);
        }

        bool didRaceInThatYear = await _context.RaceResults
            .AnyAsync(rr => rr.TeamId == id && rr.Year == year);

        if (!didRaceInThatYear)
        {
            throw new TeamDidNotRaceInThatYearException(context.Request.Path);
        }

        return await _context.RaceResults
            .Where(rr => rr.Year == year && rr.TeamId == id)
            .Include(rr => rr.Driver)
            .Include(rr => rr.Team)
            .Include(rr => rr.Circuit)
            .OrderBy(rr => rr.Date)
            // Move DNFs to the bottom of the list
            .ThenBy(rr => rr.Position == 0 ? int.MaxValue : rr.Position)
            .Select(rr => new RaceResultDto()
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
