using EduCenterModel.BaseEnum;
using EduCenterModel.User;
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

        public string CurrentScheduleTypeName { get; set; }

        public UserRole UserRole { get; set; }

        public EUserAccount UserAccount { get; set; }

        /// <summary>
        /// 是否今天第一次购买课时，课程下节课计算从第二天开始
        /// </summary>
        public bool IsBuyCourseToday { get; set; }

        /// <summary>
        /// 会员类型
        /// </summary>
        public MemberType MemeberType { get; set; }

    }
}
