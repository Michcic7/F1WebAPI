using API.Data;
using API.Data.Models;
using API.Data.ModelViews;
using API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class RaceService
{
	private readonly F1WebAPIContext _context;

	public RaceService(F1WebAPIContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<RaceView>> GetRaces(int year)
	{
		List<Race> races = new();
		
		try
		{
			races = await
				_context.Races.Where(r => r.StandingsYear.StandingsYearId == year).ToListAsync();
		}
		catch (ArgumentNullException)
		{
			throw new InvalidYearException();
		}

		Queue<RaceView> raceViews = new();

		foreach (var race in races)
		{
			RaceView raceView = new(
				race.Name,
				race.Date,
				race.WinnerFirstName + " " + race.WinnerLastName,
				race.Car,
				race.Laps,
				race.Time);

			raceViews.Enqueue(raceView);
		}

		return raceViews;
	}
}
