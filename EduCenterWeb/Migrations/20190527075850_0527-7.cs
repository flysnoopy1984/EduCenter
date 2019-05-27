using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _05277 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "CourseSchedule");

            migrationBuilder.RenameColumn(
                name: "CourseScheduleType",
                table: "CourseSchedule",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "CourseScheduleStatus",
                table: "CourseSchedule",
                newName: "Day");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CourseSchedule",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CourseSchedule");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "CourseSchedule",
                newName: "CourseScheduleType");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "CourseSchedule",
                newName: "CourseScheduleStatus");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CourseSchedule",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
