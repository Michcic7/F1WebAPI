using System.Text.Json.Serialization;

namespace API.ExtensionMethods;

public static class ControllersSetup
{
    public static IServiceCollection AddControllersConfiguration(
        this IServiceCollection services)
    {
        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });

        services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        return services;
    }
}
