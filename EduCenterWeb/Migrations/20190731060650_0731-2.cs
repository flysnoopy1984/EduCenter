using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _07312 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AliPayApplication",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServerUrl = table.Column<string>(maxLength: 256, nullable: true),
                    AppId = table.Column<string>(maxLength: 32, nullable: true),
                    AppName = table.Column<string>(maxLength: 100, nullable: true),
                    Merchant_Private_Key = table.Column<string>(nullable: true),
                    Merchant_Public_key = table.Column<string>(nullable: true),
                    Version = table.Column<string>(maxLength: 10, nullable: true),
                    SignType = table.Column<string>(maxLength: 10, nullable: true),
                    Charset = table.Column<string>(maxLength: 10, nullable: true),
                    RecordStatus = table.Column<int>(nullable: false),
                    AuthUrl_Store = table.Column<string>(maxLength: 256, nullable: true),
                    IsCurrent = table.Column<bool>(nullable: false),
                    IsSubAccount = table.Column<bool>(nullable: false),
                    SupportHuaBei = table.Column<bool>(nullable: false),
                    SupportTransfer = table.Column<bool>(nullable: false),
                    AccountForSub = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AliPayApplication", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AliPayApplication");
        }
    }
}
