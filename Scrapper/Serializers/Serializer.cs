using Newtonsoft.Json;
using ScrapperConsoleApp.Models;
using ScrapperConsoleApp.Scrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapperConsoleApp.Serializers;

internal static class Serializer
{
	public static void SerializeDrivers()
	{
		List<Driver> drivers = new();
		List<Driver> driversToAdd = new();
		DriverStandingsScrapper scrapper = new();

		for (int year = 1950; year <= 2022; year++)
		{
			driversToAdd = scrapper.GetDriverStandings(year);

			drivers.AddRange(driversToAdd);
		}

		var json = JsonConvert.SerializeObject(drivers, Formatting.Indented);

		File.WriteAllText("drivers.json", json);
	}

	public static void SerializeTeams()
	{
		List<Team> teams = new();
		List<Team> teamsToAdd = new();
		TeamStandingsScrapper scrapper = new();

		for (int year = 1958; year <= 2022; year++)
		{
			teamsToAdd = scrapper.GetTeamStandings(year);

			teams.AddRange(teamsToAdd);
		}

		var json = JsonConvert.SerializeObject(teams, Formatting.Indented);

		File.WriteAllText("teams.json", json);
	}

	public static void SerializeRaces()
	{
		List<Race> races = new();
		List<Race> racesToAdd = new();
		RaceResultsScrapper scrapper = new();

		for (int year = 1950; year <= 2022; year++)
		{
			racesToAdd = scrapper.GetRacesResults(year);

			races.AddRange(racesToAdd);
		}

		var json = JsonConvert.SerializeObject(races, Formatting.Indented);

		File.WriteAllText("races.json", json);
	}
}
