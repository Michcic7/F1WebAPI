namespace API.ProblemDetailsErrors;

public class StandingYearErrorFeature
{
    public string Type { get; }
    public string Title { get; }
    public string Detail { get; }

    public StandingYearErrorType ErrorType { get; set; }
}

public enum StandingYearErrorType
{
    DriverStanding,
    TeamStanding
}