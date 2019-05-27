using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _05373 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "TecSkill");

            migrationBuilder.AddColumn<int>(
                name: "CourseType",
                table: "TecSkill",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseType",
                table: "TecSkill");

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "TecSkill",
                nullable: true);
        }
    }
}
