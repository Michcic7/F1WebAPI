using API.Data.Models;

namespace API.Data.DTOs;

public class DriverStandingDto
{
    //public int DriverStandingId { get; set; }
    //public int Year { get; set; }
    public int Position { get; set; }
    public Driver Driver { get; set; }
    public float Points { get; set; }
}
