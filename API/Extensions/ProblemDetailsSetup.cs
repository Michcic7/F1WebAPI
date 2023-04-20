using Hellang.Middleware.ProblemDetails;
using API.CustomExceptions;
using API.CustomExceptions.CustomProblemDetails;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.Data.Models;

namespace API.Extensions;

public static class ProblemDetailsSetup
{
    public static IServiceCollection AddProblemDetailsConfiguration(
            this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (context, exception) => false;

            MapException<NonPositivePageNumberException, NonPositivePageNumberDetails>(options);
            MapException<PageNumberExceededTotalPagesException, PageNumberExceededTotalPagesDetails>(options);

            MapException<InvalidYearException, InvalidYearDetails>(options);
            MapException<InvalidEntityIdException, InvalidEntityIdDetails>(options);
            MapException<EntityNotFoundException, EntityNotFoundDetails>(options);

            MapException<DriverWithoutStandingsException, DriverWithoutStandingsDetails>(options);
            MapException<TeamWithoutStandingsException, TeamWithoutStandingsDetails>(options);

            MapException<DriverDidNotRaceInThatYearException, DriverDidNotRaceInThatYearDetails>(options);
            MapException<TeamDidNotRaceInThatYearException, TeamDidNotRaceInThatYearDetails>(options);
            MapException<CircuitNoRaceInThatYearException, CircuitNoRaceInThatYearDetails>(options);
        });

        return services;
    }

    private static void MapException<TException, TDetails>(
        Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options)
        where TException : Exception, IExceptionProperties
        where TDetails : ProblemDetails, new()
    {
        options.Map<TException>(exception => new TDetails
        {
            Type = exception.Type,
            Title = exception.Title,
            Status = exception.Status,
            Detail = exception.Detail,
            Instance = exception.Instance
        });
    }
}
