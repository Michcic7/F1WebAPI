using API.Data.Models;
using API.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Data;

public class DataSeeder
{
    private readonly string _jsonFolder = Path.Combine(
        DirectoryHelper.GetProjectFolderPath(), "Data", "Json");

    public void SeedInitialData()
    {
        using (var context = new F1WebAPIContext())
        {
            List<Driver> drivers = DeserializeDrivers();
            List<Team> teams = DeserializeTeams();
            List<Circuit> circuits = DeserializeCircuits();

            List<DriverStanding> driverStandings = DeserializeDriverStandings();
            List<TeamStanding> teamStandings = DeserializeTeamStandings();
            List<RaceResult> raceResults = DeserializeRaceResults();

            context.Drivers.AddRange(drivers);
            context.Teams.AddRange(teams);
            context.Circuits.AddRange(circuits);

            // Preserve json relationships in DriverStandings.
            foreach (var driverStanding in driverStandings)
            {
                Driver driver = driverStanding.Driver;
                Team team = driverStanding.Team;

                Driver? existingDriver = drivers.FirstOrDefault(d =>
                    d.DriverId == driverStanding.DriverId);

                if (existingDriver != null)
                {
                    driver = existingDriver;
                }
                else
                {
                    context.Attach(driver);
                }

                Team? existingTeam = teams.FirstOrDefault(t =>
                    t.TeamId == team.TeamId);

                if (existingTeam != null)
                {
                    team = existingTeam;
                }
                else
                {
                    context.Attach(team);
                }

                context.DriverStandings.Add(new DriverStanding
                {
                    Year = driverStanding.Year,
                    Position = driverStanding.Position,
                    Driver = driver,
                    Team = team,
                    Points = driverStanding.Points,
                });

                context.Entry(driver).State = EntityState.Detached;
            }


            // Preserve json relationships in TeamStandings.
            foreach (var teamStanding in teamStandings)
            {
                Team team = teamStanding.Team;

                Team? existingTeam = teams.FirstOrDefault(t =>
                    t.TeamId == teamStanding.TeamId);

                if (existingTeam != null)
                {
                    team = existingTeam;
                }
                else
                {
                    context.Attach(team);
                }

                context.TeamStandings.Add(new TeamStanding
                {
                    Year = teamStanding.Year,
                    Position = teamStanding.Position,
                    Team = team,
                    Points = teamStanding.Points
                });

                context.Entry(team).State = EntityState.Detached;
            }


            // Preserve json relationships in RaceResults.
            foreach (var raceResult in raceResults)
            {
                Driver driver = raceResult.Driver;
                Team team = raceResult.Team;
                Circuit circuit = raceResult.Circuit;

                Driver? existingDriver = drivers.FirstOrDefault(d =>
                    d.DriverId == raceResult.DriverId);

                if (existingDriver != null)
                {
                    driver = existingDriver;
                }
                else
                {
                    context.Attach(driver);
                }

                Team? existingTeam = teams.FirstOrDefault(t =>
                    t.TeamId == raceResult.TeamId);

                if (existingTeam != null)
                {
                    team = existingTeam;
                }
                else
                {
                    context.Attach(team);
                }

                Circuit? existingCircuit = circuits.FirstOrDefault(c =>
                    c.CircuitId == raceResult.CircuitId);

                if (existingCircuit != null)
                {
                    circuit = existingCircuit;
                }
                else
                {
                    context.Attach(circuit);
                }

                context.RaceResults.Add(new RaceResult
                {
                    RaceResultId = raceResult.RaceResultId,
                    Year = raceResult.Year,
                    Position = raceResult.Position,
                    Circuit = circuit,
                    Date = raceResult.Date,
                    Driver = driver,
                    Team = team,
                    Laps = raceResult.Laps,
                    Time = raceResult.Time,
                    Points = raceResult.Points
                });
            }

            context.SaveChanges();
        }

        Console.WriteLine("Database created and seeded.");
    }

    private List<RaceResult> DeserializeRaceResults()
    {
        List<RaceResult>? raceResults = new();

        string path = Path.Combine(_jsonFolder, "raceResults.json");

        using (StreamReader reader = new(path))
        {
            string json = reader.ReadToEnd();
            raceResults = JsonConvert.DeserializeObject<List<RaceResult>>(json)!;
        }

        foreach (var raceResult in raceResults)
        {
            raceResult.CircuitId = raceResult.Circuit.CircuitId;
            raceResult.DriverId = raceResult.Driver.DriverId;
            raceResult.TeamId = raceResult.Team.TeamId;
        }

        return raceResults;
    }

    private List<TeamStanding> DeserializeTeamStandings()
    {
        List<TeamStanding>? teamStandings = new();

        string path = Path.Combine(_jsonFolder, "teamStandings.json");

        using (StreamReader reader = new(path))
        {
            string json = reader.ReadToEnd();
            teamStandings = JsonConvert.DeserializeObject<List<TeamStanding>>(json)!;
        }

        foreach (var teamStanding in teamStandings)
        {
            teamStanding.TeamId = teamStanding.Team.TeamId;
        }

        return teamStandings;
    }

    private List<DriverStanding> DeserializeDriverStandings()
    {
        List<DriverStanding>? driverStandings = new();

        string path = Path.Combine(_jsonFolder, "driverStandings.json");

        using (StreamReader reader = new(path))
        {
            string json = reader.ReadToEnd();
            driverStandings = JsonConvert.DeserializeObject<List<DriverStanding>>(json)!;
        }

        foreach (var driverStanding in driverStandings)
        {
            driverStanding.DriverId = driverStanding.Driver.DriverId;
            driverStanding.TeamId = driverStanding.Team.TeamId;
        }

        return driverStandings;
    }

    private List<Circuit> DeserializeCircuits()
    {
        List<Circuit>? circuits = new();

        string path = Path.Combine(_jsonFolder, "circuits.json");

        using (StreamReader reader = new(path))
        {
            string json = reader.ReadToEnd();
            circuits = JsonConvert.DeserializeObject<List<Circuit>>(json)!;
        }

        return circuits;
    }

    private List<Team> DeserializeTeams()
    {
        List<Team>? teams = new();

        string path = Path.Combine(_jsonFolder, "teams.json");

        using (StreamReader reader = new(path))
        {
            string json = reader.ReadToEnd();
            teams = JsonConvert.DeserializeObject<List<Team>>(json)!;
        }

        return teams;
    }

    private List<Driver> DeserializeDrivers()
    {
        List<Driver>? drivers = new();

        string path = Path.Combine(_jsonFolder, "drivers.json");

        using (StreamReader reader = new(path))
        {
            string json = reader.ReadToEnd();
            drivers = JsonConvert.DeserializeObject<List<Driver>>(json)!;
        }

        return drivers;
    }
}
