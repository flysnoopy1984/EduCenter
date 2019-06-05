using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06043 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TecOpenId",
                table: "TecCourse");

            migrationBuilder.AddColumn<string>(
                name: "TecCode",
                table: "TecCourse",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TecCode",
                table: "TecCourse");

            migrationBuilder.AddColumn<string>(
                name: "TecOpenId",
                table: "TecCourse",
                maxLength: 32,
                nullable: true);
        }
    }
}
