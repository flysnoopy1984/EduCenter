using EduCenterModel.Course;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Pages.User
{
    public class PUserApply
    {
        public List<ECourseTime> CourseTimeList { get; set; }

        public List<ECourseSchedule> CourseScheduleList { get; set; }
    }
}
