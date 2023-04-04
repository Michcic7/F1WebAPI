using API.Bootstrap;
using API.Data;
using API.Data.Models;
using API.Services;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
	.AddSwagger()
	.AddProblemDetailsConfiguration()
	.AddDbContext(builder)
	.AddTransient<IDriverService, DriverService>()
	.AddTransient<ITeamService, TeamService>()
	.AddTransient<ICircuitService, CircuitService>()
	.AddTransient<IDriverStandingService, DriverStandingService>()
	.AddControllers();

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
