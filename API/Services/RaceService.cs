using API.Data;
using API.Data.Models;
using API.Data.DTOs;
using API.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API.Services;

public interface IRaceService
{
	Task<IEnumerable<RaceDto>> GetRaces(int year);
}

public class RaceService : IRaceService
{
	private readonly F1WebAPIContext _context;

	public RaceService(F1WebAPIContext context)
	{
		_context = context;
	}
		
	public async Task<IEnumerable<RaceDto>> GetRaces(int year)
	{
		if (year < 1950 || year > 2022)
		{
			throw new ArgumentException("The year must be between 1950 and 2022");
		}

		List<Race> races = await
				_context.Races.Where(r => r.StandingsYear.StandingsYearId == year).ToListAsync();

		if (races.Count is 0)
		{
			throw new ArgumentNullException();
		}

		return races.Select(r =>
		{
			return new RaceDto()
			{
				Name = r.Name,
				Date = r.Date,
				WinnerName = $"{r.WinnerFirstName} {r.WinnerLastName}",
				Car = r.Car,
				Laps = r.Laps,
				Time = r.Time
			};
		});
	}
}
