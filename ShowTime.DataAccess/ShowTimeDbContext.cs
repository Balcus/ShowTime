using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Configurations;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess;

public class ShowTimeDbContext : DbContext
{
    public ShowTimeDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Lineup> Lineup { get; set; }
    public DbSet<Festival> Festival { get; set; }
    public DbSet<Artist> Artist { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowTimeDbContext).Assembly);
        new ArtistConfiguration().Configure(modelBuilder.Entity<Artist>());
        new LineupConfiguration().Configure(modelBuilder.Entity<Lineup>());
        new FestivalConfigration().Configure(modelBuilder.Entity<Festival>());
    }
}