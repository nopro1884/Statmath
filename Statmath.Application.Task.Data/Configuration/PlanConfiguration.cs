using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Statmath.Application.Task.Models;

namespace Statmath.Application.Task.Data.Configuration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(prop => prop.Id)
                .HasName("id");

            builder.Property(prop => prop.Machine)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(prop => prop.Job)
                .IsRequired();

            builder.Property(prop => prop.StartedAt)
                .HasColumnType("TIMESTAMP(0)");

            builder.Property(prop => prop.EndedAt)
                .HasColumnType("TIMESTAMP(0)");
        }
    }
}