using API.Interfaces;

namespace API.CustomExceptions;

public class PageNumberExceededTotalPagesException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public PageNumberExceededTotalPagesException(string instance)
    {
        Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1";
        Title = "Invalid page number.";
        Status = StatusCodes.Status400BadRequest;
        Detail = "Requested page number must be less than total number of pages.";
        Instance = instance;
    }
}
