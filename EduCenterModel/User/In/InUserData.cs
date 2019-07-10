using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.In
{
    public class InUserData
    {
        public string OpenId { get; set; }

        public MemberType MemberType { get; set; }
        public UserRole UserRole { get; set; }

        public double VipPrice { get; set; }

        public double RemainTimeStd { get; set; }

        public double RemainTimeSummer { get; set; }

        public double RemainTimeWinter { get; set; }

        public string DeadLineStd { get; set; }

        public string RealName { get; set; }


    }
}
