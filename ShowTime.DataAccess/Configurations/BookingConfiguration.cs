using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Booking");
        builder.HasKey(x => new { x.FestivalId, x.UserId });
        builder.Property(x => x.Type).HasConversion<string>();
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.UserId);
        
        builder
            .HasOne(x => x.Festival)
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.FestivalId);
    }
}