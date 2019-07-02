using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Session;
using EduCenterModel.User;
using EduCenterSrv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EduCenterWeb.Pages.WebBackend
{
    public class LoginModel : PageModel
    {
        private BackendSrv _BackendSrv;

        public LoginModel(BackendSrv backendSrv)
        {
            _BackendSrv = backendSrv;
        }

        public void OnGet()
        {
            ClearUserSession();
        }

        public void SetUserSesion(EUserInfoBackEnd eUserInfoBackEnd)
        {
            BackendSession session = new BackendSession()
            {
                
                UserName = eUserInfoBackEnd.LoginName,
                UserRole = eUserInfoBackEnd.UserRole

            };
        
            var json = JsonConvert.SerializeObject(session);
            HttpContext.Session.SetString(EduConstant.BackendSessionKey, json);
        }

        public void ClearUserSession()
        {
            HttpContext.Session.Remove(EduConstant.BackendSessionKey);
        }

        public IActionResult OnPostUserLogin(string loginName, string loginPwd)
        {
            ResultObject<EUserInfoBackEnd> result = new ResultObject<EUserInfoBackEnd>();
            try
            {
                EUserInfoBackEnd eUserInfoBackEnd =  _BackendSrv.UserLogin(loginName, loginPwd);
                if (eUserInfoBackEnd == null)
                {
                    result.ErrorMsg = "用户名或密码错误！";
                }
                else if((int)eUserInfoBackEnd.UserRole <30)
                {
                    result.ErrorMsg = "权限不足";
                }
                else
                {
                    result.Entity = eUserInfoBackEnd;
                    SetUserSesion(eUserInfoBackEnd);
         
                }

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostUserLogOut()
        {
            ResultNormal result = new ResultNormal();
            try
            {

                ClearUserSession();
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}