using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06241 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSLog",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APPName = table.Column<string>(maxLength: 30, nullable: true),
                    UserPhone = table.Column<string>(maxLength: 15, nullable: true),
                    SendDateTime = table.Column<DateTime>(nullable: false),
                    RequestMessage = table.Column<string>(maxLength: 100, nullable: true),
                    ResponseMessage = table.Column<string>(maxLength: 200, nullable: true),
                    IsSuccess = table.Column<bool>(nullable: false),
                    Exception = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SMSVerification",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderNo = table.Column<string>(maxLength: 64, nullable: true),
                    VerifyCode = table.Column<string>(maxLength: 10, nullable: true),
                    SMSEvent = table.Column<int>(nullable: false),
                    MobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    SMSVerifyStatus = table.Column<int>(nullable: false),
                    SendDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSVerification", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSLog");

            migrationBuilder.DropTable(
                name: "SMSVerification");
        }
    }
}
