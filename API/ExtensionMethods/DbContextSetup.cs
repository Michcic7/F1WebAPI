using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.ExtensionMethods;

public static class DbContextSetup
{
    public static IServiceCollection AddDbContext(
        this IServiceCollection services, WebApplicationBuilder builder)
    {
        services
            .AddDbContext<F1WebAPIContext>(options =>
            {
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable(
                        "F1WebAPIConnectionString", EnvironmentVariableTarget.User));
            });

        return services;
    }
}
