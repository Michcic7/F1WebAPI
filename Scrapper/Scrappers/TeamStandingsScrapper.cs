using HtmlAgilityPack;
using ScrapperConsoleApp.Models;
using System.Text;

namespace ScrapperConsoleApp.Scrappers;

internal class TeamStandingsScrapper
{
	private int index = 1;

	public List<Team> GetTeamStandings(int year)
    {
        HtmlWeb web = new();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument document = web.Load($"https://www.formula1.com/en/results.html/{year}/team.html");

        HtmlNodeCollection rows = document.DocumentNode.SelectNodes(
            "//table[@class='resultsarchive-table']/tbody/tr");

        List<Team> teams = new();

        foreach (var item in rows)
        {
            Team team = new Team();

            try
            {
                var position = item.SelectSingleNode(
                "./td[@class='dark']").InnerText;
                var name = item.SelectSingleNode(
                    "./td/a[@class='dark bold uppercase ArchiveLink']").InnerText;
                var points = item.SelectSingleNode(
                    "./td[@class='dark bold']").InnerText;

                team.TeamId = index++;
                team.StandingsYearId = year;
                team.Position = position;
                team.Name = name;
                if (points == "null")
                    points = "0";
                team.Points = float.Parse(points);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            teams.Add(team);
        }

        return teams;
    }
}
