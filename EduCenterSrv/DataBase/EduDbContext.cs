using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Order;
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

        public DbSet<ETecSkill> DBTecSkill { get; set; }

        public DbSet<ETecCourse> DBTecCourse { get; set; }

        public DbSet<ECourseInfo> DBCourseInfo { get; set; }

        public DbSet<ECourseInfoClass> DBCourseInfoClass { get; set; }

        public DbSet<ECourseSchedule> DbCourseSchedule { get; set; }

        public DbSet<ECoursePrice> DBCoursePrice { get; set; }

        public DbSet<EUserCourse> DBUserCoures { get; set; }

        public DbSet<EUserCourseLog> DBUserCourseLog { get; set; }

        public DbSet<EOrder> DBOrder { get; set; }

        public DbSet<EOrderLine> DBOrderLine { get; set; }

        public  DbSet<EUserCourseTime> DBUserCourseTime { get; set; }

        public DbSet<EUserCourseTimeTrans> DBUserCourseTimeTrans { get; set; }

        public DbSet<ETrialLog> DBTrialLog { get; set; }

        public DbSet<EHoliday> DBHoliday { get; set; }


    }
}
