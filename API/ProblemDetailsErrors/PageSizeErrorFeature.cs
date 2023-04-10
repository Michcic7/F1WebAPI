
namespace API.ProblemDetailsErrors;

public class PageSizeErrorFeature
{
    public string Type { get; }
    public string Title { get; }
    public string Detail { get; }

    public PageSizeErrorType ErrorType { get; set; }
}

public enum PageSizeErrorType
{
    PageSizeTooSmall,
    PageSizeTooBig
}
