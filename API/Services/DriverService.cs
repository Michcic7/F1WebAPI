using API.Models;
using HtmlAgilityPack;
using System.Net;
using System.Text;

namespace API.Services;

public class DriverService
{
	public IEnumerable<Driver> GetDriverStandings(int year)
	{
		HtmlWeb web = new();
		web.OverrideEncoding = Encoding.UTF8;
		HtmlDocument document = web.Load($"https://www.formula1.com/en/results.html/{year}/drivers.html");

		HtmlNodeCollection rows = document.DocumentNode.SelectNodes(
			"//table[@class='resultsarchive-table']/tbody/tr");

		Queue<Driver> drivers = new();

		foreach (var item in rows)
		{
			Driver driver = new();

			try
			{
				var position = item.SelectSingleNode(
				"./td[@class='dark']").InnerText;
				var firstName = item.SelectSingleNode(
					"./td/a/span[@class='hide-for-tablet']").InnerText;
				var lastName = item.SelectSingleNode(
					"./td/a/span[@class='hide-for-mobile']").InnerText;
				var nationality = item.SelectSingleNode(
					"./td[@class='dark semi-bold uppercase']").InnerText;
				var team = item.SelectSingleNode(
					"./td/a[@class='grey semi-bold uppercase ArchiveLink']").InnerText;
				var points = item.SelectSingleNode(
					"./td[@class='dark bold']").InnerText;

				driver.Position = position;
				driver.Name = $"{WebUtility.HtmlDecode(firstName)} {WebUtility.HtmlDecode(lastName)}";
				driver.Nationality = nationality;
				driver.Team = team;
				if (points == "null")
					points = "0";
				driver.Points = float.Parse(points);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			drivers.Enqueue(driver);
		}

		return drivers;
	}
}