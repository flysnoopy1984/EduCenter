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
                UserCourseLogList = _UserSrv.GetUserCourseLogList(us.OpenId, UserCourseLogStatus.SignIn);
            }
        }

        public IActionResult OnPostInitPage()
        {
            ResultObject<RUserSign> result = new ResultObject<RUserSign>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    _UserSrv.GetCurrentUserSign(us.OpenId, us.CurrentScheduleType);


                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆！";
                }

            }
            catch (EduException eex)
            {
                result.ErrorMsg = eex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "页面加载失败,请联系工作人员";
                NLogHelper.ErrorTxt($"签到页面[OnPostInitPage]:{ex.Message}");
            }
            return new JsonResult(result);
        }


    }
}