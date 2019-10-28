using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _10041 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appNavigation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShowName = table.Column<string>(maxLength: 20, nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Module = table.Column<int>(nullable: false),
                    TargetAppPageName = table.Column<string>(maxLength: 50, nullable: true),
                    Position = table.Column<int>(nullable: false),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appNavigation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appNavigation");
        }
    }
}
