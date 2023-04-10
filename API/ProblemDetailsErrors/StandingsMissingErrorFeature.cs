namespace API.ProblemDetailsErrors;

public class StandingsMissingErrorFeature
{
    public string Type { get; }
    public string Title { get; }
    public string Detail { get; }

    public StandingType StandingType { get; set; }
}

public enum StandingType
{
    DriverStanding,
    TeamStanding
}