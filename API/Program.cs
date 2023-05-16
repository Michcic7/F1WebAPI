using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddSwagger()
	.AddDbContext(builder)
	.AddControllersConfiguration()
	.AddProblemDetailsConfiguration()
	.AddRateLimiterConfiguration()
	.AddTransient<IDriverService, DriverService>()
	.AddTransient<ITeamService, TeamService>()
	.AddTransient<ICircuitService, CircuitService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	using (var scope = app.Services.CreateScope())
	{
		var dbContext = scope.ServiceProvider.GetRequiredService<F1WebAPIContext>();

		if (dbContext.Database.EnsureCreated())
		{
            DataSeeder seeder = new();
            seeder.SeedInitialData();
        }
    }
}

app.UseProblemDetails();
app.UseHttpsRedirection();
app.UseRateLimiter();

app.MapControllers();

app.Run();
