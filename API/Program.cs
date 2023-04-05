using API.Bootstrap;
using API.Data;
using API.Data.Models;
using API.Services;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
	.AddSwagger()
	.AddDbContext(builder)
	.AddTransient<IDriverService, DriverService>()
	.AddTransient<ITeamService, TeamService>()
    .AddTransient<ICircuitService, CircuitService>()
	.AddTransient<IDriverStandingService, DriverStandingService>()
	.AddTransient<ITeamStandingService, TeamStandingService>()
	.AddTransient<IRaceResultService, RaceResultService>()
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

    //DataSeeder seeder = new();
    //seeder.SeedInitialData();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
