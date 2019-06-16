using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UserLeaveDateTime",
                table: "UserCourseLog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UserSignDateTime",
                table: "UserCourseLog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserLeaveDateTime",
                table: "UserCourseLog");

            migrationBuilder.DropColumn(
                name: "UserSignDateTime",
                table: "UserCourseLog");
        }
    }
}
