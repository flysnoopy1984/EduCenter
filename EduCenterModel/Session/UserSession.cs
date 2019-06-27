using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Session
{
    public class UserSession
    {
       
        public string OpenId { get; set; }

        public string UserName { get; set; }

        public string HeaderUrl { get; set; }

        public string Phone { get; set; }

        public CourseScheduleType CurrentScheduleType { get; set; }

    }
}
