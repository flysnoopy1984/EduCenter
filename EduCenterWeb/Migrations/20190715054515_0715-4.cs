using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _07154 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "QRInvite",
                newName: "FinalFilePath");

            migrationBuilder.AddColumn<string>(
                name: "FileWithLogoPath",
                table: "QRInvite",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileWithLogoPath",
                table: "QRInvite");

            migrationBuilder.RenameColumn(
                name: "FinalFilePath",
                table: "QRInvite",
                newName: "FilePath");
        }
    }
}
