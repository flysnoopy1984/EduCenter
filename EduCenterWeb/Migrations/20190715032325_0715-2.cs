using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _07152 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QRInvite",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    InviteQRType = table.Column<int>(nullable: false),
                    TargetUrl = table.Column<string>(maxLength: 256, nullable: true),
                    FilePath = table.Column<string>(maxLength: 128, nullable: true),
                    OrigFilePath = table.Column<string>(maxLength: 128, nullable: true),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRInvite", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QRInvite");
        }
    }
}
