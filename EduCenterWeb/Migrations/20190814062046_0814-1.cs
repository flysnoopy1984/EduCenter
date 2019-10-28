using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _08141 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtInfo",
                table: "ArtInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtDetail",
                table: "ArtDetail");

            migrationBuilder.RenameTable(
                name: "ArtInfo",
                newName: "miniArtInfo");

            migrationBuilder.RenameTable(
                name: "ArtDetail",
                newName: "miniArtDetail");

            migrationBuilder.RenameColumn(
                name: "OpenId",
                table: "miniArtInfo",
                newName: "UnionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_miniArtInfo",
                table: "miniArtInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_miniArtDetail",
                table: "miniArtDetail",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_miniArtInfo",
                table: "miniArtInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_miniArtDetail",
                table: "miniArtDetail");

            migrationBuilder.RenameTable(
                name: "miniArtInfo",
                newName: "ArtInfo");

            migrationBuilder.RenameTable(
                name: "miniArtDetail",
                newName: "ArtDetail");

            migrationBuilder.RenameColumn(
                name: "UnionId",
                table: "ArtInfo",
                newName: "OpenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtInfo",
                table: "ArtInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtDetail",
                table: "ArtDetail",
                column: "Id");
        }
    }
}
