using Newtonsoft.Json;
using Scrapper;

var drivers = DriverStandingsScrapper.GetDriverStandings(2020);

var json = JsonConvert.SerializeObject(drivers, Formatting.Indented);

File.WriteAllText("drivers.json", json);