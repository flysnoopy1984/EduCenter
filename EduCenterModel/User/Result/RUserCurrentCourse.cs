using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserCurrentCourse
    {
        public string UserOpenId { get; set; }

        public MemberType MemberType { get; set; }
        public string UserName { get; set; }
        public string ChildName { get; set; }

        public string LessonCode { get; set; }

        public UserCourseLogStatus UserCourseLogStatus { get; set; }

        public string UserCourseLogStatusName { get; set; }

        public string SignDateTime { get; set; }

        public int RemainTime { get; set; }

    }
}
