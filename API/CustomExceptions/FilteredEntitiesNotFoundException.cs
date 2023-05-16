using API.Interfaces;

namespace API.CustomExceptions;

public class FilteredEntitiesNotFoundException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public FilteredEntitiesNotFoundException(Type entityType, string instance)
    {
        Type = "https://httpstatuses.io/404";
        Title = $"{entityType.Name}s not found.";
        Status = StatusCodes.Status404NotFound;
        Detail = $"{entityType.Name}s with given filtering options were not found.";
        Instance = instance;
    }
}
