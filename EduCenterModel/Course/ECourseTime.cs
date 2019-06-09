using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Course
{
    public class ECourseTime
    {
        public int Lesson { get; set; }

        public string TimeRange { get; set; }

        public double StartTime { get; set; }
        public double EndTime { get; set; }
    }
}
