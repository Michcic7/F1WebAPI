using API.Bootstrap;
using API.Services;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
	.AddProblemDetailsConfiguration()
	.AddTransient<DriverService>()
	.AddTransient<TeamService>()
	.AddTransient<RaceService>()
	.AddDbContext(builder)
	.AddControllers();

services.AddSwaggerGen();

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
