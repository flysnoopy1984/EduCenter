using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class LoginModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;

        public LoginModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPostUserLogin()
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var ui = _UserSrv.GetUserInfo("o3nwE0qI_cOkirmh_qbGGG-5G6B0");

                var userAccount = _UserSrv.GetUserAccount("o3nwE0qI_cOkirmh_qbGGG-5G6B0");

                CourseScheduleType courseScheduleType = _UserSrv.GetCurrentCourseScheduleType(ui.OpenId);

                base.SetUserSesion(ui.OpenId,ui.Name, ui.wx_headimgurl,ui.Phone, courseScheduleType,ui.UserRole,ui.MemberType,userAccount);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "登陆失败";

                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

       
    }
}