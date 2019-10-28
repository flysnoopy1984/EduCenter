using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _09252 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tool_LessonQR",
                table: "tool_LessonQR");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "tool_LessonQR",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tool_LessonQR",
                table: "tool_LessonQR",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tool_LessonQR",
                table: "tool_LessonQR");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "tool_LessonQR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tool_LessonQR",
                table: "tool_LessonQR",
                column: "Id");
        }
    }
}
