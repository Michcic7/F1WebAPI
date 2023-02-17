using API.Exceptions;
using Hellang.Middleware.ProblemDetails;

namespace API.Bootstrap;

public static class ProblemDetailsSetup
{
	public static IServiceCollection AddProblemDetailsConfiguration(
		this IServiceCollection services)
	{
		services.AddProblemDetails(options =>
		{
			options.IncludeExceptionDetails = (context, exception) => false;
			options.MapToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);
			options.MapToStatusCode<ArgumentException>(StatusCodes.Status400BadRequest);
			options.MapToStatusCode<InvalidNameException>(StatusCodes.Status404NotFound);
			options.MapToStatusCode<InvalidYearException>(StatusCodes.Status404NotFound);
		});

		return services;
	}
}
