namespace API.Data.Models;

public class Driver
{
    public int DriverId { get; set; }
    public string Position { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nationality { get; set; }
    public string Team { get; set; }
    public float Points { get; set; }

    public int StandingsYearId { get; set; }
    public StandingsYear StandingsYear { get; set; }
}
