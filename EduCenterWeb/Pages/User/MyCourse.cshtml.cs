using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduCenterModel.BaseEnum;
using EduCenterModel.Course;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyCourseModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;

        public Dictionary<int, ECourseTime> CourseTime { get; set; }
        public List<RUserCourse> UserCourseList { get; set; }

        public List<RUserCourseLog> UserCourseLogList { get; set; }

        public RUserCourseLog NextCourse { get; set; }

        public RUserCourseLog CurrentCourse { get; set; }

        public Dictionary<int,string> UserCourseLogStatus { get; set; }

        public MyCourseModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
          
            var us = base.GetUserSession();
            if (us != null)
            {
                CourseTime = StaticDataSrv.CourseTime;
                UserCourseLogStatus = BaseEnumSrv.UserCourseLogStatusList;

                CourseScheduleType courseScheduleType = _UserSrv.GetCurrentCourseScheduleType(us.OpenId);
                UserCourseList = _UserSrv.GetUserCourseAvaliable(us.OpenId, courseScheduleType,false);
                if(UserCourseList.Count == 0)
                {
                    if(courseScheduleType == CourseScheduleType.Standard || courseScheduleType== CourseScheduleType.Group)
                    {
                        string url = "/User/Apply?msg="+HttpUtility.UrlEncode( "您还没有选择每周课程");
                        HttpContext.Response.Redirect(url);
                        return;
                    }
                    else if(courseScheduleType == CourseScheduleType.Summer || courseScheduleType == CourseScheduleType.Winter)
                    {
                        string url = $"/User/ApplyWinterSummer?type={(int)courseScheduleType}&msg="+ HttpUtility.UrlEncode("您还没有选择假期课程");
                        HttpContext.Response.Redirect(url);
                        return;
                    }
                         
                }
                UserCourseLogList = _UserSrv.GetUserCourseLogHistory(us.OpenId, CourseScheduleType.Standard, 10);

                //计算下一节课的时间
                foreach (var course in UserCourseList)
                {
                    course.NextCourseDate = DateSrv.GetNextCourseDate(course.Day);
              
                }
                //获取用户下次的课程
                var userLog = _UserSrv.GetNextUserCourseLog(us.OpenId, CourseScheduleType.Standard);
                if(userLog.CourseDateTime == DateTime.Today.ToString("yyyy-MM-dd"))
                 {
                    CurrentCourse = new RUserCourseLog
                    {
                        CourseName = userLog.CourseName,
                        CourseTime = userLog.CourseTime,
                        CourseDateTime = userLog.CourseDateTime,
                        UserCourseLogStatus = userLog.UserCourseLogStatus,
                    };
                 }
                else
                {
                    NextCourse = new RUserCourseLog
                    {
                        CourseName = userLog.CourseName,
                        CourseTime = DateTime.Parse(userLog.CourseDateTime).ToString("MM月dd日"),
                        CourseDateTime = userLog.CourseDateTime,
                        UserCourseLogStatus = userLog.UserCourseLogStatus,
                    };
                }
          
            }
            else
                UserCourseList = new List<RUserCourse>();
        }

        public string GetDayIconFont(int day)
        {
            switch(day)
            {
                case 1: return "#icon-zhouyi";
                case 2: return "#icon-zhouer";
                case 3: return "#icon-zhousan";
                case 4: return "#icon-zhousi";
                case 5: return "#icon-zhouwu";
                case 6: return "#icon-zhouliu";
                case 7: return "#icon-zhouri";
            }
            return "";
            
        }
    }
}