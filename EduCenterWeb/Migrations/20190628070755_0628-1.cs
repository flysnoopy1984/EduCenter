using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06281 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCourseStatus",
                table: "UserCourse");

            migrationBuilder.AddColumn<DateTime>(
                name: "BuyDate",
                table: "UserAccount",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SummerBuyDate",
                table: "UserAccount",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WinterBuyDate",
                table: "UserAccount",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyDate",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "SummerBuyDate",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "WinterBuyDate",
                table: "UserAccount");

            migrationBuilder.AddColumn<int>(
                name: "UserCourseStatus",
                table: "UserCourse",
                nullable: false,
                defaultValue: 0);
        }
    }
}
