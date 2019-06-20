using EduCenterModel.Common;
using EduCenterModel.Course;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Pages.WebBackEnd
{
    public class PPlan
    {
        public List<ECourseSchedule> CourseScheduleList { get; set; }

        //public ECourseDateRange CourseDateRange { get; set; }

        public string PlanInfo { get; set; }
    }
}
