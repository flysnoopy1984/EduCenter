using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserList
    {
        public string userOpenId { get; set; }

        public string WxName { get; set; }

        public string RealName { get; set; }

        public string BabyName { get; set; }


        public MemberType MemberType { get; set; }

       
        public UserRole UserRole { get; set; }
        /// <summary>
        /// 查看是否购买过课时
        /// </summary>
        public string UserRoleName { get; set; }

        public double VipPrice { get; set; }

        public double RemainTimeStd { get; set; }

        public double RemainTimeSummer { get; set; }

        public double RemainTimeWinter { get; set; }

        public string DeadLineStd { get; set; }

        public string DeadLineSummer { get; set; }

        public string DeadLineWinter { get; set; }

        public bool AllowChooseStd { get; set; }

        public bool AllChooseWS { get; set; }

        public string WXJoinDateTime { get; set; }

        public string SalesName { get; set; }

        public string SalesOpenId { get; set; }

        public string UserPhone { get; set; }

    


    }
}
