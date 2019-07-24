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

        private static List<SiKsV> _MemberTypeList;
        public static List<SiKsV> MemberTypeList
        {
            get
            {
                if(_MemberTypeList == null)
                {
                    _MemberTypeList = new List<SiKsV>();
                    foreach (MemberType mt in Enum.GetValues(typeof(MemberType)))
                    {
                        _MemberTypeList.Add(new SiKsV
                        {
                            Key = (int)mt,
                            Value = GetMemberTypeName(mt),
                        });
                    }

                }
                return _MemberTypeList;
            }
        }

        private static List<SiKsV> _UserRoleList;
        public static List<SiKsV> UserRoleList
        {
            get
            {
                if (_UserRoleList == null)
                {
                    _UserRoleList = new List<SiKsV>();
                    foreach (UserRole ur in Enum.GetValues(typeof(UserRole)))
                    {
                        _UserRoleList.Add(new SiKsV
                        {
                            Key = (int)ur,
                            Value = GetUserRoleName(ur),
                        });
                    }

                }
                return _UserRoleList;
            }
        }

        public static string GetUserCourseLogStatusNameForTec(UserCourseLogStatus status)
        {
            string v = "";
            switch (status)
            {
                case UserCourseLogStatus.Absent:
                    v = "学生缺席";
                    break;
                case UserCourseLogStatus.PreNext:
                    v = "未签到";
                    break;
                case UserCourseLogStatus.SignIn:
                    v = "已签到";
                    break;
                case UserCourseLogStatus.Leave:
                    v = "申请请假";
                    break;
                case UserCourseLogStatus.TecLeave:
                    v = "老师请假，课程取消";
                    break;

            }
            return v;
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
                    return "国画";
                 
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
                if(v!="")
                {
                    r.Add(new SiKsV
                    {
                        Key = (int)ct,
                        Value = v,
                    });
                }
              
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
                    return "用户申请";
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
                case CourseScheduleType.SummerWinter:
                    return "寒暑假班";


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

        public static string GetCourseSkipReasonName(CourseSkipReason type)
        {
            switch (type)
            {
                case CourseSkipReason.Holiday:
                    return "节假日";
                case CourseSkipReason.TecLeave:
                    return "老师请假";
                case CourseSkipReason.UserLeave:
                    return "您请假";

            }
            return "";
        }

        public static string EduErrorMessageName(EduErrorMessage type)
        {
            switch(type)
            {
                case EduErrorMessage.NoCourseTime:
                    return "课时余额不足，请去充值!";
                case EduErrorMessage.ApplyTrial_SameTypeExist:
                    return "同类型课已经存在试听";
                case EduErrorMessage.ApplyTrial_OverAllLimit:
                    return "超过试听课总数6次";
                case EduErrorMessage.ApplyTrial_OverSingleLimit:
                    return "超过单节试听课2次";
            }
            return "";
        }

        public static string GetMemberTypeName(MemberType memberType)
        {
            switch (memberType)
            {
                case MemberType.BusinessMember:
                    return "企业用户";
                case MemberType.VIP:
                    return "VIP";
                case MemberType.Normal:
                    return "普通会员";
            }
            return "";
        }
        public static string GetUserRoleName(UserRole userRole)
        {
            switch(userRole)
            {
                case UserRole.Admin:
                    return "管理员";
                case UserRole.Assist:
                    return "前台";
                case UserRole.Member:
                    return "会员";
                case UserRole.Sales:
                    return "销售/推广";
                case UserRole.Teacher:
                    return "老师";
                case UserRole.Visitor:
                    return "访客";
                 
            }
            return "";
        }
        public static string GetInviteStatusName(InviteStatus inviteStatus)
        {
            switch (inviteStatus)
            {
                case InviteStatus.BindPhone:
                    return "手机绑定";
                case InviteStatus.ApplyTrial:
                    return "报名试听";
                case InviteStatus.Invited:
                    return "邀请加入";
                case InviteStatus.Paied:
                    return "正式入学";
            }
            return "";
        }


    }
}
