using API.Bootstrap;
using API.Services;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
	.AddSwagger()
	.AddProblemDetailsConfiguration()
	.AddDbContext(builder)
	.AddTransient<IDriverService, DriverService>()
	.AddTransient<ITeamService, TeamService>()
	.AddTransient<IRaceService, RaceService>()
	.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseProblemDetails();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
