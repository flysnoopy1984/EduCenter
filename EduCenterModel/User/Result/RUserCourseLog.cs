﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserCourseLog:EUserCourseLog
    {
        public RUserCourseLog()
        {
            UserCourseLogStatus = BaseEnum.UserCourseLogStatus.PreNext;
        }

        public string CourseName { get; set; }

        public string CourseTime { get; set; }

        public string UserCourseStatusName { get; set; }
      
        public int Day { get; set; }

        public int Lesson { get; set; }
       
    }
}
