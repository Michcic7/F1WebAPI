namespace API.ExtensionMethods.BuilderServices;

public static class ControllersSetup
{
    public static IServiceCollection AddControllersConfiguration(
        this IServiceCollection services)
    {
        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            //options.SuppressMapClientErrors = true;
        });

        return services;
    }
}
