using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _09121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppSystem = table.Column<int>(nullable: false),
                    LoginKey = table.Column<string>(maxLength: 20, nullable: true),
                    Pwd = table.Column<string>(maxLength: 20, nullable: true),
                    WxOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    Token = table.Column<string>(maxLength: 32, nullable: true),
                    EffectDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogin");
        }
    }
}
