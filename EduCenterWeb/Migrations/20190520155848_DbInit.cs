using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseInfo",
                columns: table => new
                {
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    TypeName = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInfo", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CourseSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseCode = table.Column<string>(maxLength: 20, nullable: true),
                    CourseName = table.Column<string>(maxLength: 20, nullable: true),
                    TeaOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    CourseScheduleStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseTrying",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    ResponseTeaId = table.Column<long>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTrying", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeaSkill",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TecCode = table.Column<string>(nullable: true),
                    CourseCode = table.Column<string>(nullable: true),
                    SkillLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeaSkill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TecInfo",
                columns: table => new
                {
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    TecCode = table.Column<string>(maxLength: 20, nullable: false),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    WxName = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecInfo", x => x.TecCode);
                });

            migrationBuilder.CreateTable(
                name: "TecLeave",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TecCode = table.Column<string>(nullable: true),
                    LeaveStatus = table.Column<int>(nullable: false),
                    CourseScheduleId = table.Column<long>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecLeave", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OpenId = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    UserRole = table.Column<int>(nullable: false),
                    ChildName = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfoBackEnd",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoginName = table.Column<string>(maxLength: 30, nullable: true),
                    LoginPwd = table.Column<string>(maxLength: 30, nullable: true),
                    LastLoginDateTime = table.Column<DateTime>(nullable: false),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoBackEnd", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseInfo");

            migrationBuilder.DropTable(
                name: "CourseSchedule");

            migrationBuilder.DropTable(
                name: "CourseTrying");

            migrationBuilder.DropTable(
                name: "TeaSkill");

            migrationBuilder.DropTable(
                name: "TecInfo");

            migrationBuilder.DropTable(
                name: "TecLeave");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserInfoBackEnd");
        }
    }
}
