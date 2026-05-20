using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaNails.Domain.Models;

namespace SpaNails.Infrastructure.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).IsRequired().HasConversion<string>();
            builder.Property(x => x.Notes).HasMaxLength(500);

            builder.HasOne(x => x.Client)
                   .WithMany()
                   .HasForeignKey(x => x.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Manicurist)
                   .WithMany()
                   .HasForeignKey(x => x.ManicuristId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Service)
                   .WithMany()
                   .HasForeignKey(x => x.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
