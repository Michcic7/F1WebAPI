using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using API.Data.Models;

namespace API.Data;

public class F1WebAPIContext : DbContext
{
    public F1WebAPIContext()
    {
    }

    public F1WebAPIContext (DbContextOptions<F1WebAPIContext> options)
        : base(options)
    {
	}
        
    public virtual DbSet<Driver> Drivers { get; set; } = default!;
    public virtual DbSet<Team> Teams { get; set; } = default!;
    public virtual DbSet<Circuit> Circuits { get; set; } = default!;
    public virtual DbSet<DriverStanding> DriverStandings { get; set; } = default!;
    public virtual DbSet<TeamStanding> TeamStandings { get; set; } = default!;
    public virtual DbSet<RaceResult> RaceResults { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)        
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("F1WebAPIConnectionString",
            EnvironmentVariableTarget.Machine));

        optionsBuilder.EnableSensitiveDataLogging(true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.Entity<DriverStanding>()
            .HasOne(ds => ds.Driver)
            .WithMany(d => d.DriverStandings)
            .HasForeignKey(ds => ds.DriverId);            

        modelBuilder.Entity<TeamStanding>()
            .HasOne(ts => ts.Team)
            .WithMany(t => t.TeamStandings)
            .HasForeignKey(ts => ts.TeamId);


        modelBuilder.Entity<RaceResult>()
            .HasOne(rs => rs.Driver)
            .WithMany(d => d.RaceResults)
            .HasForeignKey(rs => rs.DriverId);
        
        modelBuilder.Entity<RaceResult>()
            .HasOne(rs => rs.Team)
            .WithMany(t => t.RaceResults)
            .HasForeignKey(rs => rs.TeamId);

        modelBuilder.Entity<RaceResult>()
            .HasOne(rs => rs.Circuit)
            .WithMany(c => c.RaceResults)
            .HasForeignKey(rs => rs.CircuitId);
    }    
}
