using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Pages.User;
using EduCenterModel.Session;
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

        public string CourseScheduleTypeName
        {
            get
            {
                var us = base.GetUserSession(false);
                if(us!=null)
                {
                    return us.CurrentScheduleTypeName;
                }
                return "";
            }
        }

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
                UserCourseLogList = _UserSrv.GetUserCourseLogHistory(us.OpenId,10); 
            }
           
        }

        public IActionResult OnPostInitData()
        {
            ResultObject<PMyCourse> result = new ResultObject<PMyCourse>();
            try
            {
                var us = GetUserSession(false);
                if (us != null)
                {

                    CourseScheduleType courseScheduleType = _UserSrv.GetCurrentCourseScheduleType(us.OpenId);
                    //获取用户课程
                    result.Entity.UserCourseList = _UserSrv.GetUserCourseAvaliable(us.OpenId, courseScheduleType);
                    result.IntMsg = (int)courseScheduleType;
                    //获取用户最近课程
                    if (result.Entity.UserCourseList.Count > 0)
                    {
                        DateTime startDate = DateTime.Now;
                        if (us.IsBuyCourseToday)
                            startDate = startDate.AddDays(1);
                        result.Entity.UserShowCourse = _UserSrv.GetNextUserCourse(result.Entity.UserCourseList, startDate);

                    }
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