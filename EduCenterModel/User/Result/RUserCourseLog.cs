using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserCourseLog:EUserCourseLog
    {
        public RUserCourseLog()
        {
            UserCourseLogStatus = BaseEnum.UserCourseLogStatus.PreNext;
            UserLeaveDateTime = DateTime.MinValue;
            UserSignDateTime = DateTime.MinValue;
        }

        public string CourseName { get; set; }

        public string CourseTime { get; set; }

        public string UserCourseLogStatusName { get; set; }

        public double StartTime { get; set; }
        public double EndTime { get; set; }
      
        public int Day { get; set; }

        public int Lesson { get; set; }

        public string CourseScheduleTypeName { get; set; }

        public string LeaveDateTimeStr
        {
            get
            {
                return UserLeaveDateTime.ToString("yyyy-MM-dd hh:mm");
            }
        }

        public string SignDateTimeStr
        {
            get
            {
                return UserSignDateTime.ToString("yyyy-MM-dd hh:mm");
            }
        }




    }
}
