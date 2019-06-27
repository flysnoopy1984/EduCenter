using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserSign
    {
        public string CourseDate { get; set; }

        public string CourseName { get; set; }

        public CourseScheduleType CourseScheduleType { get; set; }

        public string StartTime { get; set; }

        public bool CanSign { get; set; }
    }
}
