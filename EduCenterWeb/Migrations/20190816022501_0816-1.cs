using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _08161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoverFilePath",
                table: "miniArtInfo",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Praize",
                table: "miniArtInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "miniArtDetail",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "miniArtComment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RefId = table.Column<long>(nullable: false),
                    ArtId = table.Column<long>(nullable: false),
                    UnionId = table.Column<string>(maxLength: 32, nullable: true),
                    Content = table.Column<string>(maxLength: 255, nullable: true),
                    Praize = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_miniArtComment", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "miniArtComment");

            migrationBuilder.DropColumn(
                name: "Praize",
                table: "miniArtInfo");

            migrationBuilder.AlterColumn<string>(
                name: "CoverFilePath",
                table: "miniArtInfo",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "miniArtDetail",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 120,
                oldNullable: true);
        }
    }
}
