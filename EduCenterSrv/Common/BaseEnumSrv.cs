using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv.Common
{
    public class BaseEnumSrv
    {
        private static List<SiKsV> _SkillLevelList;
        public static  List<SiKsV> SkillLevelList
        {
            get
            {
                if (_SkillLevelList == null)
                    _SkillLevelList = GetSkillLevel();
                return _SkillLevelList;
            }
        }

        private static List<SiKsV> _CourseTypeList;
        public static List<SiKsV> CourseTypeList
        {
            get
            {
                if (_CourseTypeList == null)
                    _CourseTypeList = GetCourseType();
                return _CourseTypeList;
            }
        }

        private static List<SiKsV> _CourseScheduleTypeList;
        public static List<SiKsV> CourseScheduleTypeList
        {
            get
            {
                if (_CourseScheduleTypeList == null)
                    _CourseScheduleTypeList = GetCourseScheduleType();
                return _CourseScheduleTypeList;
            }
        }

        private static Dictionary<int, string> _UserCourseLogStatusList;
        public static Dictionary<int,string> UserCourseLogStatusList
        {
            get
            {
                if(_UserCourseLogStatusList == null)
                    _UserCourseLogStatusList = GetUserCourseLogStatus();
                return _UserCourseLogStatusList;
            }
        }

        public static string GetUserCourseLogStatusName(UserCourseLogStatus status)
        {
            string v = "";
            switch (status)
            {
                case UserCourseLogStatus.Absent:
                    v = "学生缺席";
                    break;
                case UserCourseLogStatus.PreNext:
                    v = "准备上课";
                    break;
                case UserCourseLogStatus.SignIn:
                    v = "学生签到";
                    break;
                case UserCourseLogStatus.Leave:
                    v = "申请请假";
                    break;
                case UserCourseLogStatus.TecLeave:
                    v = "老师请假";
                    break;

            }
            return v;
        }
        public static Dictionary<int, string> GetUserCourseLogStatus()
        {
            Dictionary<int, string> r = new Dictionary<int, string>();
            foreach (UserCourseLogStatus status in Enum.GetValues(typeof(UserCourseLogStatus)))
            {
                string v = GetUserCourseLogStatusName(status);
               
                r.Add((int)status, v);
            }
            return r;
        }

        private static List<SiKsV> GetSkillLevel()
        {
            List<SiKsV> r = new List<SiKsV>();
            foreach(SkillLevel sk in Enum.GetValues(typeof(SkillLevel)) )
            {
                string v = "";
                switch(sk)
                {
                    case SkillLevel.Main:
                        v = "主技能";
                        break;
                    case SkillLevel.Good:
                        v = "一般";
                        break;
                    case SkillLevel.None:
                        v = "无";
                        break;
                    case SkillLevel.Greate:
                        v = "熟练";
                        break;
      

                }
                r.Add(new SiKsV
                {
                    Key = (int)sk,
                    Value = v,
                });
            }
            return r;
        }

        public static string GetCourseTypeName(int key)
        {
            return GetCourseTypeName((CourseType)key);
        }
        public static string GetCourseTypeName(CourseType key)
        {
            switch (key)
            {
                case CourseType.MS:
                    return "美术";
                 
                case CourseType.SF:
                    return "书法";
                  
                case CourseType.WQ:
                    return "围棋";
                    
                default:
                    return "";
                   


            }
        }

        private static List<SiKsV> GetCourseType()
        {
            List<SiKsV> r = new List<SiKsV>();
            foreach (CourseType ct in Enum.GetValues(typeof(CourseType)))
            {
                string v = GetCourseTypeName(ct);
             
                r.Add(new SiKsV
                {
                    Key = (int)ct,
                    Value = v,
                });
            }
            return r;
        }

        private static List<SiKsV> GetCourseScheduleType()
        {
            List<SiKsV> r = new List<SiKsV>();
            foreach (CourseScheduleType ct in Enum.GetValues(typeof(CourseScheduleType)))
            {
                string v = "";
                switch (ct)
                {
                    case CourseScheduleType.Standard:
                        v = "标准课程";
                        break;
                    case CourseScheduleType.Summer:
                        v = "暑假班";
                        break;
                    case CourseScheduleType.Winter:
                        v = "寒假班";
                        break;
                }
                r.Add(new SiKsV
                {
                    Key = (int)ct,
                    Value = v,
                });
            }
            return r;
        }

        public static string GetCoursingStatusName(TecCoursingStatus coursingStatus )
        {
            switch(coursingStatus)
            {
                case TecCoursingStatus.ForLeave:
                    return "请假";
                case TecCoursingStatus.Normal:
                    return "上课";
                case TecCoursingStatus.Holiday:
                    return "节假日";
            }
            return "";
        }

        public static string GetTrialLogStatusName(TrialLogStatus trialLogStatus)
        {
            switch(trialLogStatus)
            {
                case TrialLogStatus.Cancel:
                    return "取消";
                case TrialLogStatus.Done:
                    return "完成";
                case TrialLogStatus.TecConfirm:
                    return "已安排";
                case TrialLogStatus.UserApply:
                    return "已申请";
                case TrialLogStatus.UserNotCome:
                    return "未参加";
             
            }
            return "";
        }

        public static string GetLeaveStatusName(LeaveStatus leaveStatus)
        {
            switch (leaveStatus)
            {
                case LeaveStatus.Cannel:
                    return "取消";
                case LeaveStatus.Pass:
                    return "确认请假";
                case LeaveStatus.Submit:
                    return "教师提交";
           

            }
            return "";
        }

        public static string GetCourseScheduleTypeName(CourseScheduleType courseScheduleType)
        {
            switch (courseScheduleType)
            {
                case CourseScheduleType.Group:
                    return "团购课";
                case CourseScheduleType.Standard:
                    return "标准课";
                case CourseScheduleType.Summer:
                    return "暑假班";
                case CourseScheduleType.Winter:
                    return "寒假班";
            }
            return "";
        }
        public static string GetOrderTypeName(OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.UserCourse:
                    return "标准课套餐";
                case OrderType.UserCourse_Summer:
                    return "暑假班套餐";
                case OrderType.UserCourse_Winter:
                    return "寒假班套餐";
              
            }
            return "";
        }

  

        //public static string GetUserCourseLogStatusName(UserCourseLogStatus status)
        //{
        //    switch (status)
        //    {
        //        case UserCourseLogStatus.Absent:
        //            return "缺席";
        //        case UserCourseLogStatus.PreNext:
        //            return "准备上课";
        //        case UserCourseLogStatus.SignIn:
        //            return "已签到";
        //        //case UserCourseLogStatus.Started:
        //        //    return "已签到";
        //    }
        //    return "";
        //}
    }
}
