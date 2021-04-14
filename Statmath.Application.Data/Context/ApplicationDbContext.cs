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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=application;Username=postgres;Password=password");

        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply configuration
            modelBuilder.ApplyConfiguration(new PlanConfiguration());

            // default behavior if guid is not set on add plan entity
            modelBuilder.Entity<Plan>().Property(x => x.Id).HasDefaultValue(Guid.NewGuid());

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Plan> Plans { get; set; }
    }
}