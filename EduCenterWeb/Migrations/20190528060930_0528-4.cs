using Microsoft.EntityFrameworkCore.Migrations;

namespace EduCenterWeb.Migrations
{
    public partial class _05284 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<long>(
            //    name: "Id",
            //    table: "CourseSchedule",
            //    nullable: false,
            //    defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "LessonNo",
                table: "CourseSchedule",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Id",
            //    table: "CourseSchedule");

            migrationBuilder.DropColumn(
                name: "LessonNo",
                table: "CourseSchedule");
        }
    }
}
