using EduCenterModel.BaseEnum;
using EduCenterModel.Course.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserCouser_WithCoureSchedule
    {
        public long UserCourseId { get; set; }
        public List<SCourseSchedule> ScheduleList { get; set; }

        public string UserOpenId { get; set; }

       
        public string LessonCode { get; set; }

        public CourseScheduleType CourseScheduleType { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }

        public int Lesson { get; set; }

        public string Time { get; set; }

        public string CourseName { get; set; }
    }
}
