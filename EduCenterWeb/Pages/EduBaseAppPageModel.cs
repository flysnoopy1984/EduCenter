using EduCenterCore.EduFramework;
using EduCenterModel.Session;
using EduCenterModel.User;
using EduCenterSrv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduCenterWeb.Pages
{
    public class EduBaseAppPageModel:PageModel
    {
      
       
       public UserSession GetUserSession(bool toLoginIfError = true)
       {
            string json = HttpContext.Session.GetString(EduConstant.UserSessionKey);

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<UserSession>(json);
            else
            {
                if(toLoginIfError)
                    HttpContext.Response.Redirect("/User/Login");

            }
            return null;
             

       }

        public void SetUserSesion(string openId,string userName,string headerUrl,string phone)
        {
            UserSession session = new UserSession()
            {
                OpenId = openId,
                UserName = userName,
                HeaderUrl = headerUrl,
                Phone = phone

            };
            var json = JsonConvert.SerializeObject(session);
            HttpContext.Session.SetString(EduConstant.UserSessionKey, json);
        }
        public void SetUserSesion(UserSession session)
        {
         
            var json = JsonConvert.SerializeObject(session);
            HttpContext.Session.SetString(EduConstant.UserSessionKey, json);
        }

        public void SetUserSesion(EUserInfo ui)
        {
            UserSession session = new UserSession()
            {
                OpenId = ui.OpenId,
                UserName = ui.Name,
                HeaderUrl = ui.wx_headimgurl,
                Phone = ui.Phone,

            };
            var json = JsonConvert.SerializeObject(session);
            HttpContext.Session.SetString(EduConstant.UserSessionKey, json);
        }

        public void RefreshSession(string openId,UserSrv userSrv)
        {
           var ui =  userSrv.GetUserInfo(openId);
            SetUserSesion(ui);
        }
    }
}
