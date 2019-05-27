using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _05275 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeaOpenId",
                table: "CourseSchedule");

            migrationBuilder.AddColumn<int>(
                name: "CourseScheduleType",
                table: "CourseSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeaCode",
                table: "CourseSchedule",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseScheduleType",
                table: "CourseSchedule");

            migrationBuilder.DropColumn(
                name: "TeaCode",
                table: "CourseSchedule");

            migrationBuilder.AddColumn<string>(
                name: "TeaOpenId",
                table: "CourseSchedule",
                maxLength: 32,
                nullable: true);
        }
    }
}
