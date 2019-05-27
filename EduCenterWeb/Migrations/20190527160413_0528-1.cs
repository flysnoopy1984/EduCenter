using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _05281 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "CourseSchedule");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "CourseSchedule");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CourseSchedule",
                newName: "Lesson");

            migrationBuilder.AddColumn<int>(
                name: "CourseScheduleType",
                table: "CourseSchedule",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseScheduleType",
                table: "CourseSchedule");

            migrationBuilder.RenameColumn(
                name: "Lesson",
                table: "CourseSchedule",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "CourseSchedule",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "CourseSchedule",
                nullable: true);
        }
    }
}
