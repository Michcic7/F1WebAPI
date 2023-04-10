namespace API.ProblemDetailsErrors;

public class PageNumberErrorFeature
{
    public string Type { get; }
    public string Title { get; }
    public string Detail { get; }

    public PageNumberErrorType ErrorType { get; set; }
}

public enum PageNumberErrorType
{
    ZeroOrNegativePageNumber,
    NotExistingPageNumber
}