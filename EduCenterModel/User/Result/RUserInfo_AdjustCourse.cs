using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserInfo_AdjustCourse
    {
        public string openId { get; set; }

        public string wxName { get; set; }

        public string MemberTypeName { get; set; }

        public double RemainStd { get; set; }

        public double RemainSummer { get; set; }

        public double RemainWinter { get; set; }
    }
}
