using EduCenterModel.Course;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Pages.User
{
    public class PUserApply
    {
        public Dictionary<int, ECourseTime> CourseTimeList { get; set; }
        public Dictionary<int, int> CourseMaxApplyNum { get; set; }
        //    public List<ECourseSchedule> CourseScheduleList { get; set; }

    }
}
