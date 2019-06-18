using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Teacher.Result
{
    public class RTecLeave:ETecLeave
    {
        public string LeaveDateStr
        {
            get { return LeaveDate.ToString("yyyy-MM-dd"); }
        }

        public string ApplyDateTimeeStr
        {
            get
            {
                return ApplyDateTime.ToString("yyyy-MM-dd hh:mm:dd");
            }    
        }

       
    }
}
