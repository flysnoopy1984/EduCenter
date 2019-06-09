using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Teacher.Result
{
    public class RTecCourse:ETecCourse
    {
      
        public string CourseDateTimeStr
        {
            get
            {
                return CourseDateTime.ToString("yyyy-M-dd");
            }
        }
        public string TimeRange { get; set; }

        public string CoursingStatusName { get; set; }


    }
}
