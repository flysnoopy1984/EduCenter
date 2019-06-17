using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Teacher.Result
{
    public class RTecLeave:ETecLeave
    {
        public string LeaveStatusName { get; set; }

        public string CreateDateTimeStr {
            get
            {
                return base.CreateDateTime.ToString("yyyy-MM-dd hh:mm:dd");
            }    
        }

       
    }
}
