using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Data.Models;

public class Driver
{
    public int DriverId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Nationality { get; set; }

    [JsonIgnore]
    public List<DriverStanding> DriverStandings { get; set; }
    [JsonIgnore]
    public List<RaceResult> RaceResults { get; set; }
}