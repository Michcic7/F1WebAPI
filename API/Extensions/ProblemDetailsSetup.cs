using Hellang.Middleware.ProblemDetails;
using API.CustomExceptions;
using API.CustomExceptions.CustomProblemDetails;

namespace API.Extensions;

public static class ProblemDetailsSetup
{
    public static IServiceCollection AddProblemDetailsConfiguration(
            this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (context, exception) => false;

            MapInvalidDriverId(options);
            MapDriverNotFound(options);
            MapNonPositivePageNumber(options);
            MapPageNumberExceededTotalPages(options);
        });

        return services;
    }

    private static void MapInvalidDriverId(
        Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options)
    {
        options.Map<InvalidDriverIdException>(exception => new InvalidDriverIdDetails
        {
            Type = exception.Type,
            Title = exception.Title,
            Status = exception.Status,
            Detail = exception.Detail,
            Instance = exception.Instance
        });
    }

    private static void MapDriverNotFound(
        Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options)
    {
        options.Map<DriverNotFoundException>(exception => new DriverNotFoundDetails
        {
            Type = exception.Type,
            Title = exception.Title,
            Status = exception.Status,
            Detail = exception.Detail,
            Instance = exception.Instance
        });
    }

    private static void MapNonPositivePageNumber(
        Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options)
    {
        options.Map<NonPositivePageNumberException>(
            exception => new NonPositivePageNumberDetails
            {
                Type = exception.Type,
                Title = exception.Title,
                Status = exception.Status,
                Detail = exception.Detail,
                Instance = exception.Instance
            });
    }

    private static void MapPageNumberExceededTotalPages(
        Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options)
    {
        options.Map<PageNumberExceededTotalPagesException>(
            exception => new PageNumberExceededTotalPagesDetails
            {
                Type = exception.Type,
                Title = exception.Title,
                Status = exception.Status,
                Detail = exception.Detail,
                Instance = exception.Instance
            });
    }


    //public static IServiceCollection AddProblemDetailsConfiguration(
    //        this IServiceCollection services)
    //{
    //    services.AddProblemDetails(options =>
    //        options.CustomizeProblemDetails = (context) =>
    //        {
    //            var pageErrorFeature = context.HttpContext.Features
    //                .Get<PageNumberErrorFeature>();
    //            SetPageNumberErrorProblemDetails(context, pageErrorFeature);

    //            var pageSizeErrorFeature = context.HttpContext.Features
    //                .Get<PageSizeErrorFeature>();
    //            SetPageSizeErrorProblemDetails(context, pageSizeErrorFeature);

    //            var driverErrorIdFeature = context.HttpContext.Features
    //                .Get<DriverIdErrorFeature>();
    //            SetDriverIdErrorProblemDetails(context, driverErrorIdFeature);

    //            var standingYearErrorFeature = context.HttpContext.Features
    //                .Get<StandingYearErrorFeature>();
    //            SetStandingYearErrorProblemDetails(context, standingYearErrorFeature);

    //            var standingIdErrorFeature = context.HttpContext.Features
    //                .Get<StandingsMissingErrorFeature>();
    //            SetStandingIdErrorProblemDetails (context, standingIdErrorFeature);
    //        }
    //    );

    //    return services;
    //}

    //private static void SetPageNumberErrorProblemDetails(
    //    ProblemDetailsContext context, PageNumberErrorFeature pageErrorFeature)
    //{
    //    if (pageErrorFeature is not null)
    //    {
    //        (string Detail, string Type) details = pageErrorFeature.ErrorType switch
    //        {
    //            PageNumberErrorType.ZeroOrNegativePageNumber =>
    //                ("Page number must be greater than 0.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1"),

    //            PageNumberErrorType.NotExistingPageNumber =>
    //                ("Page number must be lower than the total number of pages.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1")
    //        };

    //        context.ProblemDetails.Type = details.Type;
    //        context.ProblemDetails.Title = "Invalid Input";
    //        context.ProblemDetails.Detail = details.Detail;
    //    }
    //}

    //private static void SetPageSizeErrorProblemDetails(
    //    ProblemDetailsContext context, PageSizeErrorFeature pageSizeErrorFeature)
    //{
    //    if (pageSizeErrorFeature is not null)
    //    {
    //        (string Detail, string Type) details = pageSizeErrorFeature.ErrorType switch
    //        {
    //            PageSizeErrorType.PageSizeTooSmall =>
    //                ("Page size must be bigger than 0.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1"),

    //            PageSizeErrorType.PageSizeTooBig =>
    //                ("Page size must be smaller than 40.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1"),

    //            _ =>
    //                ("Page size cannot be 0.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1")
    //        };

    //        context.ProblemDetails.Type = details.Type;
    //        context.ProblemDetails.Title = "Invalid Input";
    //        context.ProblemDetails.Detail = details.Detail;
    //    }
    //}

    //private static void SetDriverIdErrorProblemDetails(
    //    ProblemDetailsContext context, DriverIdErrorFeature driverIdErrorFeature)
    //{
    //    if (driverIdErrorFeature is not null)
    //    {
    //        (string Detail, string Type) details = driverIdErrorFeature.ErrorType switch
    //        {
    //            DriverIdErrorType.NotExistingId =>
    //                ("There is no driver with such an ID.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4"),

    //            DriverIdErrorType.ZeroOrNegativeId =>
    //                ("ID must be bigger than 0.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1")
    //        };

    //        context.ProblemDetails.Type = details.Type;
    //        context.ProblemDetails.Title = "Invalid ID";
    //        context.ProblemDetails.Detail = details.Detail;
    //    }
    //}

    //private static void SetStandingYearErrorProblemDetails(
    //    ProblemDetailsContext context, StandingYearErrorFeature standingYearErrorFeature)
    //{
    //    if (standingYearErrorFeature is not null)
    //    {
    //        (string Detail, string Type) details = standingYearErrorFeature.ErrorType switch
    //        {
    //            StandingYearErrorType.DriverStanding =>
    //                ("Drivers Championships took place between 1950 and 2022.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1"),

    //            StandingYearErrorType.TeamStanding =>
    //                ("Constructors Championships took place betwwen 1958 and 2022.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1")
    //        };

    //        context.ProblemDetails.Type = details.Type;
    //        context.ProblemDetails.Title = "Invalid input.";
    //        context.ProblemDetails.Detail = details.Detail;
    //    }
    //}

    //private static void SetStandingIdErrorProblemDetails(
    //    ProblemDetailsContext context, StandingsMissingErrorFeature driverStandingIdErrorFeature)
    //{
    //    if (driverStandingIdErrorFeature is not null)
    //    {
    //        (string Detail, string Type) details = driverStandingIdErrorFeature.StandingType switch
    //        {
    //            StandingType.DriverStanding =>
    //                ("The driver didn't participate in Drivers Championships.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4"),

    //            StandingType.TeamStanding =>
    //                ("The team didn't participate in Constructors Championships.",
    //                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4")
    //        };

    //        context.ProblemDetails.Type = details.Type;
    //        context.ProblemDetails.Title = "Championships not found";
    //        context.ProblemDetails.Detail = details.Detail;
    //    }
    //}
}




//private static void SetProblemDetails(
//    ProblemDetailsContext context, IProblemDetailsError errorDetails)
//{
//    var problemDetails = context.HttpContext.Features.Get<ProblemDetails>();
//    problemDetails.Type = errorDetails.Type;
//    problemDetails.Title = errorDetails.Title;
//    problemDetails.Detail = errorDetails.Detail;
//}

//private static IProblemDetailsError GetPageNumberErrorDetails(
//    PageNumberErrorType pageNumberErrorType)
//{
//    return pageNumberErrorType switch
//    {
//        PageNumberErrorType.ZeroOrNegativePageNumber =>
//            new PageNumberErrorFeature
//            {
//                Detail = "Page number must be greater than 0."
//            },
//        PageNumberErrorType.NotExistingPageNumber =>
//            new PageNumberErrorFeature
//            {
//                Detail = "Page number must be lower than the total number of pages."
//            },
//        _ =>
//            new PageNumberErrorFeature
//            {
//                Detail = "Page size cannot be 0."
//            }
//    };
//}
