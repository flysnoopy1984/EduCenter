using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Course.Result
{
    public class RTrialLog:ETrialLog
    {
        public string TrialDateStr
        {
            get { return this.TrialDateTime.ToString("yyyy-MM-dd"); }
        }

        public string TrialTimeStr
        {
            get;set;
        }

        public string ApplyDateTimeStr
        {
            get { return this.ApplyDateTime.ToString("yyyy-MM-dd hh:mm"); }
        }

        public string TrialLogStatusName { get; set; }

        public string UserPhone { get; set; }

        public string WXName { get; set; }
        public string UserRealName { get; set; }



    }
}
