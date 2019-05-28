using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _05281 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseSchedule");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "CourseSchedule",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
