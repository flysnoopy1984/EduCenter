using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserSkipCourse
    {
        public string Date { get; set; }

     
        public string LessonCode { get; set; }
        public string CourseName { get; set; }

        public CourseSkipReason  CourseSkipReason {get;set;}

        public string StartTime { get; set; }

        public string CourseSkipReasonName { get; set; }
    }
}
