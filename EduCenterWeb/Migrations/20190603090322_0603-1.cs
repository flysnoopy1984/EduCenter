using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06031 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Qty",
                table: "CoursePrice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<string>(maxLength: 50, nullable: false),
                    RefId = table.Column<string>(maxLength: 50, nullable: true),
                    PayAmount = table.Column<double>(nullable: false),
                    CustOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    OrderType = table.Column<int>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<string>(maxLength: 50, nullable: true),
                    ItemCode = table.Column<string>(maxLength: 50, nullable: true),
                    ItemName = table.Column<string>(maxLength: 50, nullable: true),
                    Qty = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Ext1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    LessonCode = table.Column<string>(maxLength: 50, nullable: true),
                    CoursePriceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourseTime",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    RemainQty = table.Column<double>(nullable: false),
                    CoursePriceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourseTimeTrans",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    TransQty = table.Column<double>(nullable: false),
                    CoursePriceType = table.Column<int>(nullable: false),
                    CoursePriceCode = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseTimeTrans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderLine");

            migrationBuilder.DropTable(
                name: "UserCourse");

            migrationBuilder.DropTable(
                name: "UserCourseTime");

            migrationBuilder.DropTable(
                name: "UserCourseTimeTrans");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "CoursePrice");
        }
    }
}
