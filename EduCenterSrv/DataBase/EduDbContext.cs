using EduCenterModel.Course;
using EduCenterModel.Teacher;
using EduCenterModel.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduCenterSrv.DataBase
{
    public class EduDbContext: DbContext
    {

        public EduDbContext(DbContextOptions options):base(options)
        {

        }
     
        public DbSet<EUserInfo> DBUserInfo { get; set; }

        public DbSet<EUserInfoBackEnd> DBUserInfoBackEnd { get; set; }

        public DbSet<ETecInfo> DBTecInfo { get; set; }

        public DbSet<ETecLeave> DBTecLeave { get; set; }

        public DbSet<ETeaSkill> DBTecSill { get; set; }

        public DbSet<ECourseInfo> DBCourseInfo { get; set; }

        public DbSet<ECourseSchedule> DbCourseSchedule { get; set; }

        public DbSet<ECourseTrying> DbCourseTrying { get; set; }
    }
}
