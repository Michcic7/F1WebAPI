using System.Reflection;
using System.Runtime.CompilerServices;

namespace API.Bootstrap;

public static class SwaggerSetup
{
	public static IServiceCollection AddSwagger(
		this IServiceCollection services)
	{
		services.AddSwaggerGen(cfg =>
		{
			// Set the comments path for the Swagger JSON and UI.
			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			cfg.IncludeXmlComments(xmlPath);
		});

		return services;
	}
}
