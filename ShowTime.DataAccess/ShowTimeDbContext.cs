using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess;

public class ShowTimeDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Lineup> Lineup { get; set; }
    public DbSet<Festival> Festival { get; set; }
    public DbSet<Artist> Artist { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Booking> Booking { get; set; }
    public DbSet<Ticket> Ticket { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowTimeDbContext).Assembly);
    }
}