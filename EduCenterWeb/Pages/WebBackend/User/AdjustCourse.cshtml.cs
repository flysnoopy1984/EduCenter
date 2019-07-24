using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.User
{
    public class AdjustCourseModel : EduBasePageModel
    {
        private UserSrv _UserSrv;
        public AdjustCourseModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            string userOpenId = Request.Query["openId"];
            if(!string.IsNullOrEmpty(userOpenId))
            {
              //  _UserSrv.GetUserCourseAvaliable()
            }
        }
    }
}