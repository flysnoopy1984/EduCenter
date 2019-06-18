using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserCourseList
    {
        public string CourseName { get; set; }
        public string CourseDate { get; set; }

        public string LessonTime { get; set; }

        public string CourseStatusName { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string OpenId { get; set; }

        public string CourseScheduleTypeName { get; set; }
    }
}
