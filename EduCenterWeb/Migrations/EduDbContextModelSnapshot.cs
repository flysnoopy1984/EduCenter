﻿// <auto-generated />
using System;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EduCenterWeb.Migrations
{
    [DbContext(typeof(EduDbContext))]
    partial class EduDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("EduCenterModel.Course.ECourseSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseCode")
                        .HasMaxLength(20);

                    b.Property<string>("CourseName")
                        .HasMaxLength(20);

                    b.Property<int>("CourseScheduleType");

                    b.Property<int>("CourseType");

                    b.Property<int>("Day");

                    b.Property<int>("Lesson");

                    b.Property<string>("TecCode")
                        .HasMaxLength(20);

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("CourseSchedule");
                });

            modelBuilder.Entity("EduCenterModel.Course.ECourseTrying", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Remark");

                    b.Property<long>("ResponseTeaId");

                    b.Property<int>("Score");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("CourseTrying");
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
                        .HasMaxLength(10);

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

                    b.Property<long>("CourseScheduleId");

                    b.Property<int>("LeaveStatus");

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("TecCode");

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
