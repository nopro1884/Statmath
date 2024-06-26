﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Statmath.Application.Data.Context;

namespace Statmath.Application.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210419210812_DeleteCascade")]
    partial class DeleteCascade
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Statmath.Application.Models.JobDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValue(new Guid("6c456067-4c0d-48b2-b02d-5de9a8fb495b"));

                    b.Property<DateTime>("EndedAt")
                        .HasColumnType("TIMESTAMP(0)");

                    b.Property<int>("Job")
                        .HasColumnType("integer");

                    b.Property<Guid>("MachineId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("TIMESTAMP(0)");

                    b.HasKey("Id")
                        .HasName("JobId");

                    b.HasIndex("Job")
                        .IsUnique();

                    b.HasIndex("MachineId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Statmath.Application.Models.MachineDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Statmath.Application.Models.JobDto", b =>
                {
                    b.HasOne("Statmath.Application.Models.MachineDto", "Machine")
                        .WithMany("Jobs")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
