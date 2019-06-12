using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MySignModel : EduBaseAppPageModel
    {
        public List<RUserCourseLog> UserCourseLogList { get; set; }
        private UserSrv _UserSrv;
        public MySignModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
       
        public void OnGet()
        {
            var us = base.GetUserSession();
            if (us == null)
                return;
            else
            {

                UserCourseLogList = _UserSrv.GetUserCourseLogList(us.OpenId, CourseScheduleType.Standard, UserCourseLogStatus.SignIn);
            }
        }
    }
}