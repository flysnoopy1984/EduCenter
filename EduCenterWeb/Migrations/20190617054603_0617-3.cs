using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06173 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseScheduleId",
                table: "TecLeave");

            migrationBuilder.AlterColumn<string>(
                name: "TecCode",
                table: "TecLeave",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "TecLeave",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LeaveDate",
                table: "TecLeave",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LessonCode",
                table: "TecLeave",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TecOffDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tecCode = table.Column<string>(maxLength: 20, nullable: true),
                    Day = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecOffDay", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TecOffDay");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "TecLeave");

            migrationBuilder.DropColumn(
                name: "LeaveDate",
                table: "TecLeave");

            migrationBuilder.DropColumn(
                name: "LessonCode",
                table: "TecLeave");

            migrationBuilder.AlterColumn<string>(
                name: "TecCode",
                table: "TecLeave",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CourseScheduleId",
                table: "TecLeave",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
