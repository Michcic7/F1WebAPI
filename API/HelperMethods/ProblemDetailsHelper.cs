using API.ProblemDetailsErrors;

namespace API.HelperMethods;

public static class ProblemDetailsHelper
{
    public static void SetPageNumberError(HttpContext httpContext, PageNumberErrorType errorType)
    {
        PageNumberErrorFeature errorFeature = new()
        {
            ErrorType = errorType
        };

        httpContext.Features.Set(errorFeature);
    }

    public static void SetPageSizeError(HttpContext httpContext, PageSizeErrorType errorType)
    {
        PageSizeErrorFeature errorFeature = new()
        {
            ErrorType = errorType
        };

        httpContext.Features.Set(errorFeature);
    }

    public static void SetDriverIdError(HttpContext httpContext, DriverIdErrorType errorType)
    {
        DriverIdErrorFeature errorFeature = new()
        {
            ErrorType = errorType
        };

        httpContext.Features.Set(errorFeature);
    }

    public static void SetStandingYearError(HttpContext httpContext, StandingYearErrorType errorType)
    {
        StandingYearErrorFeature errorFeature = new()
        {
            ErrorType = errorType
        };

        httpContext.Features.Set(errorFeature);
    }

    public static void SetStandingsMissingError(HttpContext httpContext, StandingType standingType)
    {
        StandingsMissingErrorFeature errorFeature = new()
        {
            StandingType = standingType
        };

        httpContext.Features.Set(errorFeature);
    }

}
