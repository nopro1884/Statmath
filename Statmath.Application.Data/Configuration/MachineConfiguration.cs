using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Statmath.Application.Models;

namespace Statmath.Application.Data.Configuration
{
    public class MachineConfiguration : IEntityTypeConfiguration<MachineDto>
    {
        public void Configure(EntityTypeBuilder<MachineDto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
