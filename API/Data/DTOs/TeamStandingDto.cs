using API.Data.Models;

namespace API.Data.DTOs;

public class TeamStandingDto
{
    public int Position { get; set; }
    public string Name { get; set; }
    public float Points { get; set; }
    public int Year { get; set; }
}
