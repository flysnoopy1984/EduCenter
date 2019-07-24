using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _07231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnName",
                table: "InviteLog",
                maxLength: 40,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InviteRewardTrans",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InviteLogId = table.Column<long>(nullable: false),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    Direction = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    TransDateTime = table.Column<DateTime>(nullable: false),
                    TransType = table.Column<int>(nullable: false),
                    TransStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteRewardTrans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InviteRewardTrans");

            migrationBuilder.DropColumn(
                name: "OwnName",
                table: "InviteLog");
        }
    }
}
