using API.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace API.Extensions;

public static class RateLimiterSetup
{
    public static IServiceCollection AddRateLimiterConfiguration(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            int permitLimit = 10;

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                httpContext => RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey:
                        httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                    factory: 
                        partition => new FixedWindowRateLimiterOptions()
                        {
                            AutoReplenishment = true,
                            PermitLimit = permitLimit,
                            QueueLimit = 0,
                            Window = TimeSpan.FromMinutes(1)
                        }));

            options.OnRejected = async (context, token) =>
            {
                ProblemDetails problemDetails = new()
                {
                    Type = "https://httpstatuses.io/429",
                    Title = "Rate limit exceeded.",
                    Status = StatusCodes.Status429TooManyRequests,
                    Detail = $"You have exceeded the allowed number of {permitLimit} requests in a minute.",
                    Instance = context.HttpContext.Request.Path
                };

                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsJsonAsync(problemDetails);
            };
        });

        return services;
    }
}
