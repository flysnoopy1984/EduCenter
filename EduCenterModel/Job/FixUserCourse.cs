using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Job
{
    public class FixUserCourse
    {
        public string UserOpenId { get; set; }

        public string UserName { get; set; }

        public string LessonCode { get; set; }

        public MemberType MemberType { get; set; }

        public CourseScheduleType CurrentCourseSchedule { get; set; }
    }
}
