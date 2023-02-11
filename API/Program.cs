using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StandingsContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("F1WebAPI") ?? throw new InvalidOperationException("Connection string 'F1WebAPI' not found."));
	options.EnableSensitiveDataLogging();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DriverService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
