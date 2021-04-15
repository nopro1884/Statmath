using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Statmath.Application.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValue: new Guid("4dee0cc0-e106-4536-b650-0ea4e4865826")),
                    Machine = table.Column<string>(maxLength: 10, nullable: false),
                    Job = table.Column<int>(nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TIMESTAMP(0)", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "TIMESTAMP(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_Job",
                table: "Plans",
                column: "Job",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
