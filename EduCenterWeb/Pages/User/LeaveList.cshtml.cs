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
    public class LeaveListModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;
        public LeaveListModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
           
        }

        public IActionResult OnPostQueryList(int pageIndex, int pageSize)
        {
            ResultList<RUserCourseList> result = new ResultList<RUserCourseList>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    int totalPages;
                    result.List = _UserSrv.QueryUserCourseLogList(us.OpenId, out totalPages, null, pageIndex, pageSize);
                    result.TotlaPage = totalPages;
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
                NLogHelper.ErrorTxt($"请假列表[OnPostQueryList]:{ex.Message}");
            }
            return new JsonResult(result);
        }

    }
}