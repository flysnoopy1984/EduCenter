using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06092 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CourseDateTime",
                table: "TecCourse",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CoursingStatus",
                table: "TecCourse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lesson",
                table: "TecCourse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TimeEnd",
                table: "TecCourse",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TimeStart",
                table: "TecCourse",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseDateTime",
                table: "TecCourse");

            migrationBuilder.DropColumn(
                name: "CoursingStatus",
                table: "TecCourse");

            migrationBuilder.DropColumn(
                name: "Lesson",
                table: "TecCourse");

            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "TecCourse");

            migrationBuilder.DropColumn(
                name: "TimeStart",
                table: "TecCourse");
        }
    }
}
