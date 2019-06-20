﻿// <auto-generated />
using System;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EduCenterWeb.Migrations
{
    [DbContext(typeof(EduDbContext))]
    [Migration("20190620071302_0620-1")]
    partial class _06201
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EduCenterModel.Common.ECourseDateRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseDateRangeName")
                        .HasMaxLength(30);

                    b.Property<int>("CourseScheduleType");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("CourseDateRange");
                });

            modelBuilder.Entity("EduCenterModel.Common.EHoliday", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Day");

                    b.Property<DateTime>("HolidayDate");

                    b.Property<int>("Month");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Holiday");
                });

            modelBuilder.Entity("EduCenterModel.Common.ETecOffDay", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Day");

                    b.Property<string>("tecCode")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("TecOffDay");
                });

            modelBuilder.Entity("EduCenterModel.Course.ECourseInfo", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<int>("CourseType");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<int>("RecordStatus");

                    b.Property<DateTime>("UpdatedDateTime");

                    b.HasKey("Code");

                    b.ToTable("CourseInfo");
                });

            modelBuilder.Entity("EduCenterModel.Course.ECourseInfoClass", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName")
                        .HasMaxLength(20);

                    b.Property<string>("CourseCode")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<int>("RecordStatus");

                    b.Property<string>("TecCode")
                        .HasMaxLength(20);

                    b.Property<DateTime>("UpdatedDateTime");

                    b.HasKey("Id");

                    b.ToTable("CourseInfoClass");
                });

            modelBuilder.Entity("EduCenterModel.Course.ECoursePrice", b =>
                {
                    b.Property<string>("PriceCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<int>("CourseScheduleType");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<DateTime>("EffectEndDate");

                    b.Property<DateTime>("EffectStartDate");

                    b.Property<double>("ExtQty");

                    b.Property<double>("Price");

                    b.Property<string>("PriceName")
                        .HasMaxLength(50);

                    b.Property<double>("Qty");

                    b.Property<int>("RecordStatus");

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdatedDateTime");

                    b.HasKey("PriceCode");

                    b.ToTable("CoursePrice");
                });

            modelBuilder.Entity("EduCenterModel.Course.ECourseSchedule", b =>
                {
                    b.Property<string>("LessonCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<int>("ApplyNum");

                    b.Property<string>("CourseCode")
                        .HasMaxLength(20);

                    b.Property<string>("CourseName")
                        .HasMaxLength(20);

                    b.Property<int>("CourseScheduleType");

                    b.Property<int>("CourseType");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<int>("Day");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Lesson");

                    b.Property<int>("LessonNo");

                    b.Property<int>("RecordStatus");

                    b.Property<DateTime>("UpdatedDateTime");

                    b.Property<int>("Year");

                    b.HasKey("LessonCode");

                    b.ToTable("CourseSchedule");
                });

            modelBuilder.Entity("EduCenterModel.Course.ETrialLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApplyDateTime");

                    b.Property<string>("CourseCode")
                        .HasMaxLength(20);

                    b.Property<string>("CourseName")
                        .HasMaxLength(50);

                    b.Property<int>("CourseType");

                    b.Property<int>("Lesson");

                    b.Property<string>("OpenId")
                        .HasMaxLength(32);

                    b.Property<string>("TecCode")
                        .HasMaxLength(20);

                    b.Property<string>("TecName")
                        .HasMaxLength(20);

                    b.Property<DateTime>("TrialDateTime");

                    b.Property<int>("TrialLogStatus");

                    b.Property<string>("UserComment")
                        .HasMaxLength(400);

                    b.Property<string>("UserName")
                        .HasMaxLength(50);

                    b.Property<int>("UserRank");

                    b.HasKey("Id");

                    b.ToTable("TrialLog");
                });

            modelBuilder.Entity("EduCenterModel.Order.EOrder", b =>
                {
                    b.Property<string>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("CustOpenId")
                        .HasMaxLength(32);

                    b.Property<int>("OrderStatus");

                    b.Property<int>("OrderType");

                    b.Property<double>("PayAmount");

                    b.Property<string>("RefId")
                        .HasMaxLength(50);

                    b.HasKey("OrderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("EduCenterModel.Order.EOrderLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Ext1");

                    b.Property<string>("ItemCode")
                        .HasMaxLength(50);

                    b.Property<string>("ItemName")
                        .HasMaxLength(50);

                    b.Property<string>("OrderId")
                        .HasMaxLength(50);

                    b.Property<double>("Price");

                    b.Property<double>("Qty");

                    b.HasKey("Id");

                    b.ToTable("OrderLine");
                });

            modelBuilder.Entity("EduCenterModel.Teacher.ETecCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApplyLeaveDateTime");

                    b.Property<DateTime>("CourseDateTime");

                    b.Property<string>("CourseName")
                        .HasMaxLength(20);

                    b.Property<int>("CourseScheduleType");

                    b.Property<int>("CoursingStatus");

                    b.Property<int>("Day");

                    b.Property<int>("Lesson");

                    b.Property<string>("LessonCode")
                        .HasMaxLength(50);

                    b.Property<string>("TecCode")
                        .HasMaxLength(20);

                    b.Property<double>("TimeEnd");

                    b.Property<double>("TimeStart");

                    b.HasKey("Id");

                    b.ToTable("TecCourse");
                });

            modelBuilder.Entity("EduCenterModel.Teacher.ETecInfo", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Phone")
                        .HasMaxLength(15);

                    b.Property<int>("RecordStatus");

                    b.Property<int>("Sex");

                    b.Property<DateTime>("UpdatedDateTime");

                    b.Property<string>("UserOpenId")
                        .HasMaxLength(32);

                    b.Property<string>("WxName")
                        .HasMaxLength(40);

                    b.HasKey("Code");

                    b.ToTable("TecInfo");
                });

            modelBuilder.Entity("EduCenterModel.Teacher.ETecLeave", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApplyDateTime");

                    b.Property<DateTime>("LeaveDate");

                    b.Property<int>("LeaveType");

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("TecCode")
                        .HasMaxLength(20);

                    b.Property<string>("TecName")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("TecLeave");
                });

            modelBuilder.Entity("EduCenterModel.Teacher.ETecSkill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseType");

                    b.Property<int>("SkillLevel");

                    b.Property<string>("TecCode")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("TecSkill");
                });

            modelBuilder.Entity("EduCenterModel.User.EUserCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseScheduleType");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("LessonCode")
                        .HasMaxLength(50);

                    b.Property<int>("UserCourseStatus");

                    b.Property<string>("UserOpenId")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("UserCourse");
                });

            modelBuilder.Entity("EduCenterModel.User.EUserCourseLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseDateTime")
                        .HasMaxLength(10);

                    b.Property<int>("CourseScheduleType");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("LessonCode")
                        .HasMaxLength(50);

                    b.Property<int>("UserCourseLogStatus");

                    b.Property<DateTime>("UserLeaveDateTime");

                    b.Property<string>("UserOpenId")
                        .HasMaxLength(32);

                    b.Property<DateTime>("UserSignDateTime");

                    b.HasKey("Id");

                    b.ToTable("UserCourseLog");
                });

            modelBuilder.Entity("EduCenterModel.User.EUserCourseTime", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseScheduleType");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<DateTime>("InValidDateTime");

                    b.Property<DateTime>("ReNewDateTime");

                    b.Property<double>("RemainQty");

                    b.Property<string>("UserOpenId")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("UserCourseTime");
                });

            modelBuilder.Entity("EduCenterModel.User.EUserCourseTimeTrans", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoursePriceCode")
                        .HasMaxLength(20);

                    b.Property<int>("CourseScheduleType");

                    b.Property<DateTime>("TransDateTime");

                    b.Property<double>("TransQty");

                    b.Property<string>("UserOpenId")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("UserCourseTimeTrans");
                });

            modelBuilder.Entity("EduCenterModel.User.EUserInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChildName")
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("Name")
                        .HasMaxLength(40);

                    b.Property<string>("OpenId")
                        .HasMaxLength(32);

                    b.Property<string>("Phone")
                        .HasMaxLength(15);

                    b.Property<int>("RecordStatus");

                    b.Property<int>("Sex");

                    b.Property<DateTime>("UpdatedDateTime");

                    b.Property<int>("UserRole");

                    b.Property<string>("wx_Name");

                    b.Property<string>("wx_city")
                        .HasMaxLength(20);

                    b.Property<string>("wx_country")
                        .HasMaxLength(20);

                    b.Property<string>("wx_headimgurl")
                        .HasMaxLength(256);

                    b.Property<string>("wx_province")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("EduCenterModel.User.EUserInfoBackEnd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastLoginDateTime");

                    b.Property<string>("LoginName")
                        .HasMaxLength(30);

                    b.Property<string>("LoginPwd")
                        .HasMaxLength(30);

                    b.Property<string>("UserOpenId")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("UserInfoBackEnd");
                });
#pragma warning restore 612, 618
        }
    }
}
