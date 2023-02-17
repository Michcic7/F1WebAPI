using API.Data;
using API.Data.Models;
using API.Data.DTOs;
using API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IDriverService
{
	Task<DriverCareerDto> GetDriverByName(string name);
	Task<IEnumerable<DriverDto>> GetDrivers(int year);
}

public class DriverService : IDriverService
{
	private readonly F1WebAPIContext _context;

	public DriverService(F1WebAPIContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<DriverDto>> GetDrivers(int year)
	{		
		if (year < 1950 || year > 2022)
		{
			throw new ArgumentException("The year must be between 1950 and 2022");
		}

		List<Driver> drivers = await
				_context.Drivers.Where(d => d.StandingsYear.StandingsYearId == year).ToListAsync();

		if (drivers.Count is 0)
		{
			throw new ArgumentNullException();
		}

		return drivers.Select(d =>
		{
			return new DriverDto()
			{
				Position = d.Position,
				Name = $"{d.FirstName} {d.LastName}",
				Nationality = d.Nationality,
				Team = d.Team,
				Points = d.Points
			};
		});
	}

	public async Task<DriverCareerDto> GetDriverByName(string name)
	{		
		if (name is null || name == string.Empty) 
		{
			throw new ArgumentNullException();
		}

		// Linq 'could not tanslate(?)' extension methods to remove spaces, thus string.Replace()
		Driver? driver = await _context.Drivers.FirstOrDefaultAsync(d => name == d.FirstName.Replace(" ", string.Empty) + d.LastName.Replace(" ", string.Empty));
		
		if (driver is null)
		{
			throw new InvalidNameException();
		}

		int WDCwins = _context.Drivers.Count(d => d.Position == "1" && d.FirstName.Replace(" ", string.Empty) + d.LastName.Replace(" ", string.Empty) == name);

		int raceWins = _context.Races.Count(r => r.WinnerFirstName.Replace(" ", string.Empty) + r.WinnerLastName.Replace(" ", string.Empty) == name);

		return new DriverCareerDto()
		{
			Name = $"{driver.FirstName} {driver.LastName}",
			Nationality = driver.Nationality,
			RaceWins = raceWins,
			ChampionshipWins = WDCwins
		};
	}
}
