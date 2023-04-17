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
	.AddTransient<IDriverService, DriverService>()
	.AddTransient<ITeamService, TeamService>()
	.AddTransient<ICircuitService, CircuitService>()
	.AddTransient<IDriverStandingService, DriverStandingService>()
	.AddTransient<ITeamStandingService, TeamStandingService>()
	.AddTransient<IRaceResultService, RaceResultService>();

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
app.MapControllers();

app.Run();
