using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Models;

public class Driver
{
    public int DriverId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Nationality { get; set; }

    public List<DriverStanding> DriverStandings { get; set; }
    public List<RaceResult> RaceResults { get; set; }
}