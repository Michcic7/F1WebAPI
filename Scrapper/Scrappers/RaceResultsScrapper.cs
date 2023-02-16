using HtmlAgilityPack;
using ScrapperConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapperConsoleApp.Scrappers;

internal class RaceResultsScrapper
{
	private int index = 1;
	public List<Race> GetRacesResults(int year)
	{
		HtmlWeb web = new();
		web.OverrideEncoding = Encoding.UTF8;
		HtmlDocument document = web.Load($"https://www.formula1.com/en/results.html/{year}/races.html");

		HtmlNodeCollection rows = document.DocumentNode.SelectNodes(
			"//table[@class='resultsarchive-table']/tbody/tr");

		List<Race> races = new();

		foreach (var item in rows)
		{
			Race race = new Race();

			try
			{
				var name = item.SelectSingleNode(
				"./td/a[@class='dark bold ArchiveLink']").InnerText.Trim();
				var date = item.SelectSingleNode(
					"./td[@class='dark hide-for-mobile']").InnerText;
				var winnerFirstName = item.SelectSingleNode(
					"./td[@class='dark bold']/span[@class='hide-for-tablet']").InnerText;
				var winnerLastName = item.SelectSingleNode(
					"./td[@class='dark bold']/span[@class='hide-for-mobile']").InnerText;
				var car = item.SelectSingleNode(
					"./td[@class='semi-bold uppercase ']").InnerText;
				var laps = item.SelectSingleNode(
					"./td[@class='bold hide-for-mobile']").InnerText;
				var time = item.SelectSingleNode(
					"./td[@class='dark bold hide-for-tablet']").InnerText;

				race.RaceId = index++;
				race.StandingsYearId = year;
				race.Name = name;
				race.Date = date;
				race.WinnerFirstName = winnerFirstName;
				race.WinnerLastName = winnerLastName;
				race.Car = car;
				if (laps == "null")
					laps = "0";
				race.Laps = int.Parse(laps);
				race.Time = time;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			races.Add(race);
		}

		return races;
	}
}
