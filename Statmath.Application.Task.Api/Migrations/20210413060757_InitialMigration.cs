using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Statmath.Application.Task.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValue: new Guid("eb96e1f9-4d74-4ac5-9cca-f36e86000a22")),
                    Machine = table.Column<string>(maxLength: 10, nullable: false),
                    Job = table.Column<int>(nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TIMESTAMP(0)", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "TIMESTAMP(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
