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
                CourseSkipList = new List<RUserSkipCourse>();
        }
        public string NextCourseName { get; set; }

        public string NextCourseDate { get; set; }

        public int NextLesson { get; set; }

        public string LessonCode { get; set; }

        public UserCourseLogStatus UserCourseLogStatus { get; set; }

        public string UserCourseLogStatusName { get; set; }

        public bool IsCurrent { get; set; }
        public bool CanLeave { get; set; }
        public bool CanSign { get; set; }

        public List<RUserSkipCourse> CourseSkipList { get; set; }
       

     
    }
}
