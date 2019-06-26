using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
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

              
                //UserCourseList = _UserSrv.GetUserCourseAvaliable(us.OpenId, courseScheduleType);
                //if(UserCourseList.Count == 0)
                //{
                //    if(courseScheduleType == CourseScheduleType.Standard || courseScheduleType== CourseScheduleType.Group)
                //    {
                //        string url = "/User/Apply?msg="+HttpUtility.UrlEncode( "您还没有选择每周课程");
                //        HttpContext.Response.Redirect(url);
                //        return;
                //    }
                //    else if(courseScheduleType == CourseScheduleType.Summer || courseScheduleType == CourseScheduleType.Winter)
                //    {
                //        string url = $"/User/ApplyWinterSummer?type={(int)courseScheduleType}&msg="+ HttpUtility.UrlEncode("您还没有选择假期课程");
                //        HttpContext.Response.Redirect(url);
                //        return;
                //    }       
                //}
             

                UserCourseLogList = _UserSrv.GetUserCourseLogHistory(us.OpenId, CourseScheduleType.Standard, 10);

                ////计算下一节课的时间
                //foreach (var course in UserCourseList)
                //{
                //    course.NextCourseDate = DateSrv.GetNextCourseDate(course.Day);
              
                //}
                ////获取用户下次的课程
                //var userLog = _UserSrv.GetNextUserCourseLog(us.OpenId, CourseScheduleType.Standard);
                //if(userLog.CourseDateTime == DateTime.Today.ToString("yyyy-MM-dd"))
                // {
                //    CurrentCourse = new RUserCourseLog
                //    {
                //        CourseName = userLog.CourseName,
                //        CourseTime = userLog.CourseTime,
                //        CourseDateTime = userLog.CourseDateTime,
                //        UserCourseLogStatus = userLog.UserCourseLogStatus,
                //    };
                // }
                //else
                //{
                //    NextCourse = new RUserCourseLog
                //    {
                //        CourseName = userLog.CourseName,
                //        CourseTime = DateTime.Parse(userLog.CourseDateTime).ToString("MM月dd日"),
                //        CourseDateTime = userLog.CourseDateTime,
                //        UserCourseLogStatus = userLog.UserCourseLogStatus,
                //    };
                //}
          
            }
            //else
            //    UserCourseList = new List<RUserCourse>();
        }

        public IActionResult OnPostInitData()
        {
            ResultList<RUserCourse> result = new ResultList<RUserCourse>();
            try
            {
                var us = GetUserSession(false);
                if(us !=null)
                {
                    CourseScheduleType courseScheduleType = _UserSrv.GetCurrentCourseScheduleType(us.OpenId);
                    result.List = _UserSrv.GetUserCourseAvaliable(us.OpenId, courseScheduleType);
                    result.IntMsg = (int)courseScheduleType;
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要重新登陆";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "未能获取数据！请联系客服或稍后再试";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }
    }
}