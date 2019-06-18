using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06181 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveStatus",
                table: "TecLeave");

            migrationBuilder.DropColumn(
                name: "LessonCode",
                table: "TecLeave");

            migrationBuilder.RenameColumn(
                name: "CreateDateTime",
                table: "TecLeave",
                newName: "ApplyDateTime");

            migrationBuilder.AddColumn<int>(
                name: "LeaveType",
                table: "TecLeave",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TecName",
                table: "TecLeave",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TecInfo",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveType",
                table: "TecLeave");

            migrationBuilder.DropColumn(
                name: "TecName",
                table: "TecLeave");

            migrationBuilder.RenameColumn(
                name: "ApplyDateTime",
                table: "TecLeave",
                newName: "CreateDateTime");

            migrationBuilder.AddColumn<int>(
                name: "LeaveStatus",
                table: "TecLeave",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LessonCode",
                table: "TecLeave",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TecInfo",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
