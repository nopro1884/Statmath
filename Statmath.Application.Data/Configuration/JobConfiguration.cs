using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Statmath.Application.Models;

namespace Statmath.Application.Data.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<JobDto>
    {
        public void Configure(EntityTypeBuilder<JobDto> builder)
        {
            builder.HasKey(prop => prop.Id)
                .HasName("JobId");

            builder.HasOne(m => m.Machine)
                .WithMany(p => p.Jobs)
                .HasForeignKey(k => k.MachineId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(prop => prop.Job)
                .IsRequired();

            builder.Property(prop => prop.StartedAt)
                .HasColumnType("TIMESTAMP(0)");

            builder.Property(prop => prop.EndedAt)
                .HasColumnType("TIMESTAMP(0)");

            builder.HasIndex(prop => prop.Job).IsUnique();
        }
    }
}