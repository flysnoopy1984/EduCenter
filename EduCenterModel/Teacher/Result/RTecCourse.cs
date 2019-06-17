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
                return CourseDateTime.ToString("yyyy-MM-dd");
            }
        }
        public string TecName { get; set; }
        public string TimeRange { get; set; }

        public string CoursingStatusName { get; set; }

        public string ApplyLeaveDateTimeStr
        {
            get
            {
                return ApplyLeaveDateTime.ToString("yyyy-MM-dd");
            }
        }


    }
}
