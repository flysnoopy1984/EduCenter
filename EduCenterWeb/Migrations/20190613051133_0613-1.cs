using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _06131 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTrying");

            migrationBuilder.CreateTable(
                name: "TrialLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TecCode = table.Column<string>(maxLength: 20, nullable: true),
                    TecName = table.Column<string>(maxLength: 20, nullable: true),
                    CourseCode = table.Column<string>(maxLength: 20, nullable: true),
                    CourseName = table.Column<string>(maxLength: 50, nullable: true),
                    OpenId = table.Column<string>(maxLength: 32, nullable: true),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Lesson = table.Column<int>(nullable: false),
                    TrialDateTime = table.Column<DateTime>(nullable: false),
                    TrialLogStatus = table.Column<int>(nullable: false),
                    UserComment = table.Column<string>(maxLength: 400, nullable: true),
                    UserRank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrialLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrialLog");

            migrationBuilder.CreateTable(
                name: "CourseTrying",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    ResponseTeaId = table.Column<long>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTrying", x => x.Id);
                });
        }
    }
}
