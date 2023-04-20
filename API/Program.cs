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

    //DataSeeder seeder = new();
    //seeder.SeedInitialData();
}

app.UseProblemDetails();
app.UseHttpsRedirection();
app.UseRateLimiter();

app.MapControllers();

app.Run();
