using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv
{
    public static class StaticDataSrv
    {
        private static List<EHoliday> _Holiday;
        private static List<ECourseTime> _CourseTime;

        public static List<ECourseTime> CourseTime
        {
            get
            {
                if (_CourseTime == null)
                {
                    _CourseTime = new List<ECourseTime>();
                    _CourseTime.Add(new ECourseTime{Lesson = 1,TimeRange = "9:00-10:30"});
                    _CourseTime.Add(new ECourseTime { Lesson = 2, TimeRange = "10:30-12:00" });
                    _CourseTime.Add(new ECourseTime { Lesson = 3, TimeRange = "13:00-14:30" });
                    _CourseTime.Add(new ECourseTime { Lesson = 4, TimeRange = "14:30-16:00" });
                    _CourseTime.Add(new ECourseTime { Lesson = 5, TimeRange = "16:30-18:00" });
                    _CourseTime.Add(new ECourseTime { Lesson = 6, TimeRange = "18:30-20:00" });
                 
                }
                   
                return _CourseTime;
            }
        }

        public static List<EHoliday> GetEHolidays()
        {
            //static string connection = @"Server=2013-20150707DJ\SQL2012EXPRESS;Database=AppDb;Trusted_Connection=True;";
            //4         static DbContextOptions<AppDbContext> dbContextOption = new DbContextOptions<AppDbContext>();
            //5         static DbContextOptionsBuilder<AppDbContext> dbContextOptionBuilder = new DbContextOptionsBuilder<AppDbContext>(dbContextOption);
            //6         AppDbContext _dbContext = new AppDbContext(dbContextOptionBuilder.UseSqlServer(connection).Options);

            return _Holiday;
        }
    }
}
