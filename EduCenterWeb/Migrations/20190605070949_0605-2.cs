using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06052 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoursePriceType",
                table: "UserCourseTimeTrans");

            migrationBuilder.DropColumn(
                name: "CoursePriceType",
                table: "UserCourseTime");

            migrationBuilder.DropColumn(
                name: "CoursePriceType",
                table: "UserCourse");

            migrationBuilder.DropColumn(
                name: "CoursePriceType",
                table: "CoursePrice");

            migrationBuilder.AddColumn<int>(
                name: "CourseScheduleType",
                table: "UserCourseTimeTrans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseScheduleType",
                table: "UserCourseTime",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseScheduleType",
                table: "UserCourseLog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "UserCourseLog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CourseScheduleType",
                table: "UserCourse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseScheduleType",
                table: "CoursePrice",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseScheduleType",
                table: "UserCourseTimeTrans");

            migrationBuilder.DropColumn(
                name: "CourseScheduleType",
                table: "UserCourseTime");

            migrationBuilder.DropColumn(
                name: "CourseScheduleType",
                table: "UserCourseLog");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "UserCourseLog");

            migrationBuilder.DropColumn(
                name: "CourseScheduleType",
                table: "UserCourse");

            migrationBuilder.DropColumn(
                name: "CourseScheduleType",
                table: "CoursePrice");

            migrationBuilder.AddColumn<int>(
                name: "CoursePriceType",
                table: "UserCourseTimeTrans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoursePriceType",
                table: "UserCourseTime",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoursePriceType",
                table: "UserCourse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoursePriceType",
                table: "CoursePrice",
                nullable: false,
                defaultValue: 0);
        }
    }
}
