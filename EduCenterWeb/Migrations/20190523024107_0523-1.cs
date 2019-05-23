using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _05231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "UserInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "wx_Name",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wx_city",
                table: "UserInfo",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wx_country",
                table: "UserInfo",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wx_headimgurl",
                table: "UserInfo",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wx_province",
                table: "UserInfo",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "wx_Name",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "wx_city",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "wx_country",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "wx_headimgurl",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "wx_province",
                table: "UserInfo");
        }
    }
}
