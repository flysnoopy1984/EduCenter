﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserCourseLog:EUserCourseLog
    {
       
        public string CourseName { get; set; }
        public string CreatedDateTimeStr
        {
            get
            {
                return CreatedDateTime.ToString("yyyy年MM月dd日");
            }
        }
    }
}
