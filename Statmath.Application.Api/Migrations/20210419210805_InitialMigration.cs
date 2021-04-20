using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Statmath.Application.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValue: new Guid("8eb58dc5-d201-4b29-8caf-056030c9eb83")),
                    MachineId = table.Column<Guid>(nullable: false),
                    Job = table.Column<int>(nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TIMESTAMP(0)", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "TIMESTAMP(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("JobId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Job",
                table: "Jobs",
                column: "Job",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_MachineId",
                table: "Jobs",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_Name",
                table: "Machines",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
