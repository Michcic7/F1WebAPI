using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public class StandingsContext : DbContext
{
    public StandingsContext (DbContextOptions<StandingsContext> options)
        : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; } = default!;
    public DbSet<Team> Teams { get; set; } = default!;
    public DbSet<Race> Races { get; set; } = default!;
}
