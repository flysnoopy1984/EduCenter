using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06032 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TransDateTime",
                table: "UserCourseTimeTrans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "UserCourseTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InValidDateTime",
                table: "UserCourseTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReNewDateTime",
                table: "UserCourseTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "UserCourse",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserCourseStatus",
                table: "UserCourse",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransDateTime",
                table: "UserCourseTimeTrans");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "UserCourseTime");

            migrationBuilder.DropColumn(
                name: "InValidDateTime",
                table: "UserCourseTime");

            migrationBuilder.DropColumn(
                name: "ReNewDateTime",
                table: "UserCourseTime");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "UserCourse");

            migrationBuilder.DropColumn(
                name: "UserCourseStatus",
                table: "UserCourse");
        }
    }
}
