using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Statmath.Application.Api.Migrations
{
    public partial class DeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Jobs",
                nullable: false,
                defaultValue: new Guid("6c456067-4c0d-48b2-b02d-5de9a8fb495b"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("8eb58dc5-d201-4b29-8caf-056030c9eb83"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Jobs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("8eb58dc5-d201-4b29-8caf-056030c9eb83"),
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("6c456067-4c0d-48b2-b02d-5de9a8fb495b"));
        }
    }
}
