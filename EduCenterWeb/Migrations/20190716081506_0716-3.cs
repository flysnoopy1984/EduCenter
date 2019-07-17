using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _07163 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesOpenId",
                table: "TrialLog");

            migrationBuilder.AddColumn<string>(
                name: "SalesOpenId",
                table: "UserInfo",
                maxLength: 32,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesOpenId",
                table: "UserInfo");

            migrationBuilder.AddColumn<string>(
                name: "SalesOpenId",
                table: "TrialLog",
                maxLength: 32,
                nullable: true);
        }
    }
}
