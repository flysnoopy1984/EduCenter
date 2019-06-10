using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserCourse: EUserCourse
    {
       

        public int Day { get; set; }

        public int Lesson { get; set; }

        public string Time { get; set; }

        public string CourseName { get; set; }

        public DateTime LastCourseDate { get; set; }
        public string LastCouseStatus { get; set; }

        public DateTime NextCourseDate { get; set; }

        public double StartTime { get; set; }
        public double EndTime { get; set; }

    }
}
