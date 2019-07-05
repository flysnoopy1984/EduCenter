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

        public string TecCode { get; set; }

        public string UserName { get; set; }

        public string HeaderUrl { get; set; }

        public string Phone { get; set; }

        public CourseScheduleType CurrentScheduleType { get; set; }

        public string CurrentScheduleTypeName { get; set; }

        public UserRole UserRole { get; set; }

        public EUserAccount UserAccount { get; set; }

        /// <summary>
        /// 课时是否跳过今天
        /// </summary>
        public bool CourseSkipToday { get; set; }

        /// <summary>
        /// 会员类型
        /// </summary>
        public MemberType MemeberType { get; set; }

        public static int NeedRecharge(UserSession us, CourseScheduleType courseScheduleType)
        {
            if(us.MemeberType != MemberType.VIP)
            {
               if(courseScheduleType ==  CourseScheduleType.Summer && us.UserAccount.RemainSummerTime <=0)
               {
                    return -1;
               }
               else if (courseScheduleType == CourseScheduleType.Winter && us.UserAccount.RemainWinterTime <= 0)
               {
                    return -1;
               }
               else
                {
                    if (courseScheduleType == CourseScheduleType.Standard && us.UserAccount.RemainCourseTime <= 0)
                        return -1;
                }
            }
            else
            {
                if (courseScheduleType == CourseScheduleType.Summer && us.UserAccount.RemainSummerTime <= 0
                    && us.UserAccount.RemainCourseTime <= 0)
                {
                    return -2;
                }
                else if (courseScheduleType == CourseScheduleType.Winter && us.UserAccount.RemainWinterTime <= 0
                    && us.UserAccount.RemainCourseTime <= 0)
                {
                    return -2;
                }
                else
                {
                    if (courseScheduleType == CourseScheduleType.Standard && us.UserAccount.RemainCourseTime <= 0)
                        return -2;
                }
            }
            return 0;
        }

    }
}
