using EduCenterModel.Course;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv
{
    public static class StaticDataSrv
    {
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
    }
}
