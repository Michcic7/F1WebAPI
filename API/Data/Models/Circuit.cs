namespace API.Data.Models;

public class Circuit
{
    public int CircuitId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public List<RaceResult> RaceResults { get; set; }
}
