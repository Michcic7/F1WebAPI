﻿using Hellang.Middleware.ProblemDetails;
using API.CustomExceptions;
using API.CustomExceptions.CustomProblemDetails;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.CustomExceptions.AuthExceptions;
using API.CustomExceptions.AuthExceptions.AuthProblemDetails;

namespace API.ExtensionMethods;

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
            MapException<FilteredEntitiesNotFoundException, FilteredEntitiesNotFoundDetails>(options);

            MapException<DriverWithoutStandingsException, DriverWithoutStandingsDetails>(options);
            MapException<TeamWithoutStandingsException, TeamWithoutStandingsDetails>(options);

            MapException<DriverDidNotRaceInThatYearException, DriverDidNotRaceInThatYearDetails>(options);
            MapException<TeamDidNotRaceInThatYearException, TeamDidNotRaceInThatYearDetails>(options);
            MapException<CircuitNoRaceInThatYearException, CircuitNoRaceInThatYearDetails>(options);

            // Auth exceptions.
            MapException<ExpiredRefreshTokenException, ExpiredRefreshTokenDetails>(options);
            MapException<UsernameTakenException, UsernameTakenDetails>(options);
            MapException<InvalidUserCredentialsException, InvalidUserCredentialsDetails>(options);
            MapException<InvalidAccessTokenException, InvalidAccessTokenDetails>(options);
            MapException<EmptyUserCredentialsException, EmptyUserCredentialsDetails>(options);
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
