using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class SignListModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;

        public SignListModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPostQueryList(int pageIndex, int pageSize)
        {
            ResultList<RUserCourseLog> result = new ResultList<RUserCourseLog>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    int totalPages;
     
                    result.List = _UserSrv.GetUserCourseLogList(us.OpenId, UserCourseLogStatus.SignIn, out totalPages, pageIndex, pageSize);
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
                NLogHelper.ErrorTxt($"签到列表[OnPostQueryList]:{ex.Message}");
            }
            return new JsonResult(result);
        }
    }
}