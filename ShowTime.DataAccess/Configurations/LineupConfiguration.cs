using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Configurations;

public class LineupConfiguration : IEntityTypeConfiguration<Lineup>
{
    public void Configure(EntityTypeBuilder<Lineup> builder)
    {
        builder.ToTable("Lineup");
        builder.HasKey(l => new { l.FestivalId, l.ArtistId });

        builder.Property(l => l.Stage).HasMaxLength(255);

        builder
            .HasOne(l => l.Festival)
            .WithMany(f => f.Lineups)
            .HasForeignKey(l => l.FestivalId);

        builder
            .HasOne(l => l.Artist)
            .WithMany(a => a.Lineups)
            .HasForeignKey(l => l.ArtistId);
    
    }
}