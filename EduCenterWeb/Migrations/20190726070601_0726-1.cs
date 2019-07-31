using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _07261 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SignName",
                table: "UserCourseLog",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignOpenId",
                table: "UserCourseLog",
                maxLength: 32,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignName",
                table: "UserCourseLog");

            migrationBuilder.DropColumn(
                name: "SignOpenId",
                table: "UserCourseLog");
        }
    }
}
