using API.Data.Models;
using API.Interfaces;

namespace API.CustomExceptions;

public class EntityNotFoundException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public EntityNotFoundException(Type entityType, int entityId, string instance)
    {
        Type = "https://httpstatuses.io/404";
        Title = $"{entityType.Name} not found.";
        Status = StatusCodes.Status404NotFound;
        Detail = $"{entityType.Name} with ID = {entityId} was not found.";
        Instance = instance;
    }
}


//public EntityNotFoundException(Type entityType, string instance)
//{
//    Type = "https://httpstatuses.io/404";
//    Title = $"{typeof(T).Name} not found.";
//    Status = StatusCodes.Status404NotFound;
//    Detail = $"{typeof(T).Name} with ID = {entityId} was not found.";
//    Instance = instance;
//}