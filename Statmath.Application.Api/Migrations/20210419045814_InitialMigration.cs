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
                    Id = table.Column<Guid>(nullable: false, defaultValue: new Guid("452dd4c6-728b-4a4d-a934-8a96f899d38c")),
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
