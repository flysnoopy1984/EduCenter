using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _07153 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseDateRange",
                table: "CourseDateRange");

            migrationBuilder.AlterColumn<string>(
                name: "CourseDateRangeName",
                table: "CourseDateRange",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourseDateRange",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseDateRange",
                table: "CourseDateRange",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseDateRange",
                table: "CourseDateRange");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseDateRange");

            migrationBuilder.AlterColumn<string>(
                name: "CourseDateRangeName",
                table: "CourseDateRange",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseDateRange",
                table: "CourseDateRange",
                column: "CourseDateRangeName");
        }
    }
}
