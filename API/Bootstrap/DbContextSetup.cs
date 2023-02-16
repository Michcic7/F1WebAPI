using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Bootstrap;

public static class DbContextSetup
{
	public static IServiceCollection AddDbContext(
		this IServiceCollection services, WebApplicationBuilder builder)
	{
		services
			.AddDbContext<F1WebAPIContext>(options =>
			{
				options.UseSqlServer(
					builder.Configuration.GetConnectionString("F1WebAPI") ?? 
					throw new InvalidOperationException("Connection string 'F1WebAPI' not found."));
			});

		return services;
	}
}
