using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _08271 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTime",
                table: "miniNewsInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "miniNewsInfo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "miniNewsInfo");

            migrationBuilder.AlterColumn<string>(
                name: "CreateDateTime",
                table: "miniNewsInfo",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
