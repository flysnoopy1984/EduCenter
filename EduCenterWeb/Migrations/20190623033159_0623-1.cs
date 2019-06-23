using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalCourseTime",
                table: "UserAccount",
                newName: "RemainWinterTime");

            migrationBuilder.RenameColumn(
                name: "RemainBalance",
                table: "UserAccount",
                newName: "RemainSummerTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeadLine",
                table: "UserAccount",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SummerDeadLine",
                table: "UserAccount",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WinterDeadLine",
                table: "UserAccount",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadLine",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "SummerDeadLine",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "WinterDeadLine",
                table: "UserAccount");

            migrationBuilder.RenameColumn(
                name: "RemainWinterTime",
                table: "UserAccount",
                newName: "TotalCourseTime");

            migrationBuilder.RenameColumn(
                name: "RemainSummerTime",
                table: "UserAccount",
                newName: "RemainBalance");
        }
    }
}
