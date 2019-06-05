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

        public MyCourseModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            var us = base.GetUserSession();
            if (us != null)
            {
                UserCourseList = _UserSrv.GetUserCourseAvaliable(us.OpenId,CourseScheduleType.Standard);
                UserCourseLogList = _UserSrv.GetUserCourseLog(us.OpenId, CourseScheduleType.Standard,10);

                //计算上一节课的时间和状态
                foreach(var course in UserCourseList)
                {
                    course.LastCourseDate = DateSrv.GetLastCourseDate(course.Day);
                    var courseLog = UserCourseLogList.Where(a=>a.LessonCode == course.LessonCode).OrderByDescending(a=>a.CreatedDateTime).FirstOrDefault();
                    if (courseLog == null)
                        course.LastCouseStatus = "";
                    else
                    {
                        if (courseLog.UserCourseLogStatus == UserCourseLogStatus.PreNext)
                            courseLog.UserCourseLogStatus = UserCourseLogStatus.Absent;
                        course.LastCouseStatus = BaseEnumSrv.UserCourseLogStatusList[(int)courseLog.UserCourseLogStatus];
                        
                    }    
                }
                //计算用户下节课
                NextCourse = _UserSrv.GetUserNextCourse(us.OpenId, CourseScheduleType.Standard, UserCourseList);

            }
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