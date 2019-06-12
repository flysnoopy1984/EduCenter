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

        private static Dictionary<int, string> GetUserCourseLogStatus()
        {
            Dictionary<int, string> r = new Dictionary<int, string>();
            foreach (UserCourseLogStatus status in Enum.GetValues(typeof(UserCourseLogStatus)))
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

        private static List<SiKsV> GetCourseType()
        {
            List<SiKsV> r = new List<SiKsV>();
            foreach (CourseType ct in Enum.GetValues(typeof(CourseType)))
            {
                string v = "";
                switch (ct)
                {
                    case CourseType.MS:
                        v = "美术";
                        break;
                    case CourseType.SF:
                        v = "书法";
                        break;
                    case CourseType.WQ:
                        v = "围棋";
                        break;
                    default:
                        v = "未分类";
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
