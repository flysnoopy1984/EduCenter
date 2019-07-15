using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Sales.Result
{
    public class RInviteLog:EInviteLog
    {
        public string InvitedWxName { get; set; }

        public string InvitedDateTimeStr {
            get {
                return InvitedDateTime.ToString("yyyy-MM-dd");
            }
        }
        public string InviteStatusName
        {
            get;set;
        }
    }
}
