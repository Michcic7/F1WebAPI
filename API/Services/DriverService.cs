using API.Data;
using API.Data.Models;
using API.Data.ModelViews;
using API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class DriverService
{
	private readonly F1WebAPIContext _context;

	public DriverService(F1WebAPIContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<DriverView>> GetDrivers(int year)
	{
		List<Driver> drivers = new();

		try
		{
			drivers = await 
				_context.Drivers.Where(d => d.StandingsYear.StandingsYearId == year).ToListAsync();
		}
		catch (ArgumentNullException)
		{
			throw new InvalidYearException();
		}

		Queue<DriverView> driverViews = new();

		foreach (var driver in drivers)
		{
			DriverView driverView = new(
				driver.Position,
				$"{driver.FirstName} {driver.LastName}",
				driver.Nationality,
				driver.Team,
				driver.Points);

			driverViews.Enqueue(driverView);
		}

		return driverViews;
	}

	public async Task<DriverCareerView> GetDriverByName(string name)
	{		
		Driver driver = new();
		
		try
		{
			// Linq 'could not tanslate(?)' extension methods to remove spaces, thus string.Replace()
			driver = await _context.Drivers.FirstAsync(d => name == d.FirstName.Replace(" ", string.Empty) + d.LastName.Replace(" ", string.Empty));
		}
		catch (Exception)
		{
			throw new InvalidNameException();
		}

		int WDCwins = _context.Drivers.Count(d => d.Position == "1" && d.FirstName.Replace(" ", string.Empty) + d.LastName.Replace(" ", string.Empty) == name);

		int raceWins = _context.Races.Count(r => r.WinnerFirstName.Replace(" ", string.Empty) + r.WinnerLastName.Replace(" ", string.Empty) == name);

		return new DriverCareerView($"{driver.FirstName} {driver.LastName}", driver.Nationality, raceWins, WDCwins);
	}
}
