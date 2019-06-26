using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06261 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourseTime");

            migrationBuilder.DropTable(
                name: "UserCourseTimeTrans");

            migrationBuilder.AddColumn<bool>(
                name: "CanSelectCourse",
                table: "UserAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanSelectSummerWinterCourse",
                table: "UserAccount",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanSelectCourse",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "CanSelectSummerWinterCourse",
                table: "UserAccount");

            migrationBuilder.CreateTable(
                name: "UserCourseTime",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseScheduleType = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    InValidDateTime = table.Column<DateTime>(nullable: false),
                    ReNewDateTime = table.Column<DateTime>(nullable: false),
                    RemainQty = table.Column<double>(nullable: false),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourseTimeTrans",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoursePriceCode = table.Column<string>(maxLength: 20, nullable: true),
                    CourseScheduleType = table.Column<int>(nullable: false),
                    TransDateTime = table.Column<DateTime>(nullable: false),
                    TransQty = table.Column<double>(nullable: false),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseTimeTrans", x => x.Id);
                });
        }
    }
}
