using API.Data;
using API.ExtensionMethods;
using API.Interfaces;
using API.Services;
using API.Utilities;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddSwagger()
	.AddDbContext<F1WebAPIContext>()
	.AddControllersConfiguration()
	.AddProblemDetailsConfiguration()
	.AddRateLimiterConfiguration()
	.AddJwtConfiguration(builder)
	.AddTransient<IDriverService, DriverService>()
	.AddTransient<ITeamService, TeamService>()
	.AddTransient<ICircuitService, CircuitService>()
	.AddTransient<IAuthService, AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();	
}

DirectoryHelper.Configure(app.Environment);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<F1WebAPIContext>();

	if (dbContext.Database.EnsureCreated())
	{
		DataSeeder seeder = new();
		seeder.SeedInitialData();
	}
}

app.UseProblemDetails();
app.UseHttpsRedirection();
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
