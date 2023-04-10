namespace API.ProblemDetailsErrors;

public class DriverIdErrorFeature
{
    public string Type { get; }
    public string Title { get; }
    public string Detail { get; }

    public DriverIdErrorType ErrorType { get; set; }
}

public enum DriverIdErrorType
{
    ZeroOrNegativeId,
    NotExistingId
}