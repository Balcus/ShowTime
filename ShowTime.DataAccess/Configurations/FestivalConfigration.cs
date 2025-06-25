using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Configurations;

public class FestivalConfigration : IEntityTypeConfiguration<Festival>
{
    public void Configure(EntityTypeBuilder<Festival> builder)
    {
        builder.ToTable("Festival");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Location).IsRequired().HasMaxLength(255);
        builder.Property(x => x.SplashArt).HasMaxLength(100);

        builder
            .HasMany(x => x.Artists)
            .WithMany(x => x.Festivals)
            .UsingEntity<Lineup>();
    }
}