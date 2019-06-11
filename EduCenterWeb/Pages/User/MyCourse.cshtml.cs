using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
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
        public List<RUserCourse> UserCourseList { get; set; }

        public List<RUserCourseLog> UserCourseLogList { get; set; }

        public RUserCourse NextCourse { get; set; }

        public RUserCourse CurrentCourse { get; set; }

        public MyCourseModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            var us = base.GetUserSession();
            if (us != null)
            {
                UserCourseList = _UserSrv.GetUserCourseAvaliable(us.OpenId, CourseScheduleType.Standard);
                UserCourseLogList = _UserSrv.GetUserCourseLogHistory(us.OpenId, CourseScheduleType.Standard, 10);

                //计算下一节课的时间
                foreach (var course in UserCourseList)
                {
                    course.NextCourseDate = DateSrv.GetNextCourseDate(course.Day);
              
                }
                //获取用户Log Pre的课程
                 var userLog = _UserSrv.GetNextUserCourseLog(us.OpenId, CourseScheduleType.Standard);
                 if(userLog.CourseDateTime == DateTime.Today.ToString("yyyy-MM-dd"))
                 {
                    CurrentCourse = new RUserCourse
                    {
                        CourseName = userLog.CourseName,
                        Time = userLog.CourseTime,
                    };
                 }
                 else
                {
                    NextCourse = new RUserCourse
                    {
                        CourseName = userLog.CourseName,
                        Time = DateTime.Parse(userLog.CourseDateTime).ToString("MM月dd日")
                    };
                }
             //   CurrentCourse = _UserSrv.GetCurrentUserCourse(us.OpenId, CourseScheduleType.Standard);

                //if (CurrentCourse == null)
                //{
                //    //计算用户下节课
                  
                //    NextCourse = new RUserCourse
                //    {
                //        CourseName = userLog.CourseName,
                //        Time = DateTime.Parse(userLog.CourseDateTime).ToString("MM月dd日")
                //    };
                    
                //}
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