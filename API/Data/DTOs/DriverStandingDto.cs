using API.Data.Models;

namespace API.Data.DTOs;

public class DriverStandingDto
{
    public int Position { get; set; }
    public Driver Driver { get; set; }
    public float Points { get; set; }
}
