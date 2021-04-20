using Microsoft.EntityFrameworkCore;
using Statmath.Application.Data.Configuration;
using Statmath.Application.Models;
using System;

namespace Statmath.Application.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply configuration
            modelBuilder.ApplyConfiguration(new MachineConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());

            // default behavior if guid is not set on add job entity
            modelBuilder.Entity<JobDto>().Property(x => x.Id).HasDefaultValue(Guid.NewGuid());

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<JobDto> Jobs { get; set; }

        public virtual DbSet<MachineDto> Machines { get; set; }
    }
}