using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserShowCourse
    {
        public RUserShowCourse(bool InitObject = false)
        {
            if (InitObject)
                CourseSkipList = new Dictionary<string, CourseSkipReason>();
        }
        public string NextCourseName { get; set; }

        public string NextCourseDate { get; set; }

        public int NextLesson { get; set; }

        public UserCourseLogStatus UserCourseLogStatus { get; set; }

        public bool CanLeave { get; set; }
        public bool CanSign { get; set; }

        public Dictionary<string, CourseSkipReason> CourseSkipList { get; set; }
       

     
    }
}
