using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using API.Data.Models;

namespace API.Data;

public class F1WebAPIContext : DbContext
{
    public F1WebAPIContext (DbContextOptions<F1WebAPIContext> options)
        : base(options)
    {
	}
        
    public DbSet<StandingsYear> StandingsYears { get; set; } = default!;
    public DbSet<Driver> Drivers { get; set; } = default!;
    public DbSet<Team> Teams { get; set; } = default!;
    public DbSet<Race> Races { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<Driver>()
            .HasOne(d => d.StandingsYear)
            .WithMany(y => y.Drivers);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.StandingsYear)
            .WithMany(y => y.Teams);

        modelBuilder.Entity<Race>()
            .HasOne(r => r.StandingsYear)
            .WithMany(y => y.Races);

        modelBuilder.Entity<StandingsYear>().HasData(SeedStandingsYearData());
        modelBuilder.Entity<Driver>().HasData(SeedDriverData());
        modelBuilder.Entity<Team>().HasData(SeedTeamData());
        modelBuilder.Entity<Race>().HasData(SeedRaceData());
    }

    public List<Driver> SeedDriverData()
    {
        List<Driver> drivers = new();
        using (StreamReader reader = new(@"D:\C#\F1WebAPI\API\Data\Json\drivers.json"))
        {
            string json = reader.ReadToEnd();
            drivers = JsonConvert.DeserializeObject<List<Driver>>(json);
        }

        return drivers;
    }

	public List<Team> SeedTeamData()
	{
		List<Team> teams = new();
		using (StreamReader reader = new(@"D:\C#\F1WebAPI\API\Data\Json\teams.json"))
		{
			string json = reader.ReadToEnd();
			teams = JsonConvert.DeserializeObject<List<Team>>(json);
		}

		return teams;
	}

	public List<Race> SeedRaceData()
	{
		List<Race> races = new();
		using (StreamReader reader = new(@"D:\C#\F1WebAPI\API\Data\Json\races.json"))
		{
			string json = reader.ReadToEnd();
			races = JsonConvert.DeserializeObject<List<Race>>(json);
		}

		return races;
	}

	public List<StandingsYear> SeedStandingsYearData()
	{
        List<StandingsYear> standingsYears = new();

        int yearVar = 1950;

        int years = 2023 - 1950;

        for (int i = 0; i < years; i++)
        {
            StandingsYear yearToAdd = new();
            yearToAdd.StandingsYearId = yearVar++;
            standingsYears.Add(yearToAdd);
        }

        return standingsYears;
	}
}
