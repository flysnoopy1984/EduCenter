using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Sales.Result
{
    public class RInviteLog:EInviteLog
    {
        public string InvitedWxName { get; set; }

        public string InvitedDateStr {
            get {
                return InvitedDateTime.ToString("yyyy-MM-dd");
            }
        }

        public string InvitedDateTimeStr
        {
            get
            {
                return InvitedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        public string InviteStatusName
        {
            get;set;
        }
    }
}
