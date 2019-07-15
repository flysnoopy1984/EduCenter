using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _07151 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseDateRange",
                columns: table => new
                {
                    CourseDateRangeName = table.Column<string>(maxLength: 30, nullable: false),
                    CourseScheduleType = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDateRange", x => x.CourseDateRangeName);
                });

            migrationBuilder.CreateTable(
                name: "CourseInfo",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    CourseType = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInfo", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CourseInfoClass",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    ClassName = table.Column<string>(maxLength: 20, nullable: true),
                    CourseCode = table.Column<string>(maxLength: 20, nullable: true),
                    TecCode = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInfoClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoursePrice",
                columns: table => new
                {
                    PriceCode = table.Column<string>(maxLength: 20, nullable: false),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    PriceName = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Qty = table.Column<double>(nullable: false),
                    ExtQty = table.Column<double>(nullable: false),
                    CourseScheduleType = table.Column<int>(nullable: false),
                    EffectStartDate = table.Column<DateTime>(nullable: false),
                    EffectEndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePrice", x => x.PriceCode);
                });

            migrationBuilder.CreateTable(
                name: "CourseSchedule",
                columns: table => new
                {
                    LessonCode = table.Column<string>(maxLength: 50, nullable: false),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseCode = table.Column<string>(maxLength: 20, nullable: true),
                    CourseType = table.Column<int>(nullable: false),
                    CourseName = table.Column<string>(maxLength: 20, nullable: true),
                    LessonNo = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Lesson = table.Column<int>(nullable: false),
                    ApplyNum = table.Column<int>(nullable: false),
                    CourseScheduleType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSchedule", x => x.LessonCode);
                });

            migrationBuilder.CreateTable(
                name: "Holiday",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    HolidayDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "TecCourse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LessonCode = table.Column<string>(maxLength: 50, nullable: true),
                    CourseName = table.Column<string>(maxLength: 20, nullable: true),
                    TecCode = table.Column<string>(maxLength: 20, nullable: true),
                    CourseScheduleType = table.Column<int>(nullable: false),
                    CoursingStatus = table.Column<int>(nullable: false),
                    CourseDateTime = table.Column<DateTime>(nullable: false),
                    ApplyLeaveDateTime = table.Column<DateTime>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Lesson = table.Column<int>(nullable: false),
                    TimeStart = table.Column<double>(nullable: false),
                    TimeEnd = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TecInfo",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    WxName = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecInfo", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "TecLeave",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TecCode = table.Column<string>(maxLength: 20, nullable: true),
                    TecName = table.Column<string>(maxLength: 20, nullable: true),
                    LeaveDate = table.Column<DateTime>(nullable: false),
                    ApplyDateTime = table.Column<DateTime>(nullable: false),
                    LeaveType = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecLeave", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TecSkill",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TecCode = table.Column<string>(maxLength: 20, nullable: true),
                    CourseType = table.Column<int>(nullable: false),
                    SkillLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecSkill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrialLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TecCode = table.Column<string>(maxLength: 20, nullable: true),
                    TecName = table.Column<string>(maxLength: 20, nullable: true),
                    CourseType = table.Column<int>(nullable: false),
                    CourseCode = table.Column<string>(maxLength: 20, nullable: true),
                    CourseName = table.Column<string>(maxLength: 50, nullable: true),
                    OpenId = table.Column<string>(maxLength: 32, nullable: true),
                    Lesson = table.Column<int>(nullable: false),
                    TrialDateTime = table.Column<DateTime>(nullable: false),
                    ApplyDateTime = table.Column<DateTime>(nullable: false),
                    TrialLogStatus = table.Column<int>(nullable: false),
                    UserComment = table.Column<string>(maxLength: 400, nullable: true),
                    UserRank = table.Column<int>(nullable: false),
                    WxRemindCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrialLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RemainCourseTime = table.Column<double>(nullable: false),
                    DeadLine = table.Column<DateTime>(nullable: false),
                    RemainSummerTime = table.Column<double>(nullable: false),
                    SummerDeadLine = table.Column<DateTime>(nullable: false),
                    RemainWinterTime = table.Column<double>(nullable: false),
                    WinterDeadLine = table.Column<DateTime>(nullable: false),
                    CanSelectCourse = table.Column<bool>(nullable: false),
                    CanSelectSummerWinterCourse = table.Column<bool>(nullable: false),
                    BuyDate = table.Column<DateTime>(nullable: false),
                    SummerBuyDate = table.Column<DateTime>(nullable: false),
                    WinterBuyDate = table.Column<DateTime>(nullable: false),
                    VIPPrice1 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.UserOpenId);
                });

            migrationBuilder.CreateTable(
                name: "UserChild",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    No = table.Column<int>(nullable: false),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    BirthDay = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChild", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    LessonCode = table.Column<string>(maxLength: 50, nullable: true),
                    CourseScheduleType = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UseRightNow = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourseLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LessonCode = table.Column<string>(maxLength: 50, nullable: true),
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    UserCourseLogStatus = table.Column<int>(nullable: false),
                    CourseScheduleType = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    CourseDateTime = table.Column<string>(maxLength: 10, nullable: true),
                    UserLeaveDateTime = table.Column<DateTime>(nullable: false),
                    UserSignDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    OpenId = table.Column<string>(maxLength: 32, nullable: true),
                    RealName = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    UserRole = table.Column<int>(nullable: false),
                    MemberType = table.Column<int>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    ChildName = table.Column<string>(maxLength: 50, nullable: true),
                    wx_Name = table.Column<string>(nullable: true),
                    wx_city = table.Column<string>(maxLength: 20, nullable: true),
                    wx_province = table.Column<string>(maxLength: 20, nullable: true),
                    wx_country = table.Column<string>(maxLength: 20, nullable: true),
                    wx_headimgurl = table.Column<string>(maxLength: 256, nullable: true)
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
                    UserOpenId = table.Column<string>(maxLength: 32, nullable: true),
                    UserRole = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoBackEnd", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseDateRange");

            migrationBuilder.DropTable(
                name: "CourseInfo");

            migrationBuilder.DropTable(
                name: "CourseInfoClass");

            migrationBuilder.DropTable(
                name: "CoursePrice");

            migrationBuilder.DropTable(
                name: "CourseSchedule");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderLine");

            migrationBuilder.DropTable(
                name: "SMSLog");

            migrationBuilder.DropTable(
                name: "SMSVerification");

            migrationBuilder.DropTable(
                name: "TecCourse");

            migrationBuilder.DropTable(
                name: "TecInfo");

            migrationBuilder.DropTable(
                name: "TecLeave");

            migrationBuilder.DropTable(
                name: "TecSkill");

            migrationBuilder.DropTable(
                name: "TrialLog");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "UserChild");

            migrationBuilder.DropTable(
                name: "UserCourse");

            migrationBuilder.DropTable(
                name: "UserCourseLog");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserInfoBackEnd");
        }
    }
}
