using Hellang.Middleware.ProblemDetails;
using API.CustomExceptions;
using API.CustomExceptions.CustomProblemDetails;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;

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

            MapException<InvalidDriverIdException, InvalidDriverIdDetails>(options);
            MapException<InvalidDriverStandingYearException, InvalidDriverStandingYearDetails>(options);
            MapException<InvalidRaceResultsYearException, InvalidRaceResultsYearDetails>(options);

            MapException<DriverNotFoundException, DriverNotFoundDetails>(options);
            MapException<DriverWithoutStandingsException, DriverWithoutStandingsDetails>(options);
            MapException<DriverDidNotRaceInThatYearException, DriverDidNotRaceInThatYearDetails>(options);
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
