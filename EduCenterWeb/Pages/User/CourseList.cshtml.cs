using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class CourseListModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;

        public CourseListModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPostQueryList()
        {
            ResultList<RUserCourseList> result = new ResultList<RUserCourseList>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    result.List= _UserSrv.QueryUserCourseLogList(us.OpenId);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "获取数据失败！";
                NLogHelper.ErrorTxt($"课程列表[OnPostQueryList]:{ex.Message}");
            }
            return new JsonResult(result);
        }
    }
}