using EduCenterCore.EduFramework;
using EduCenterModel.Session;
using EduCenterModel.User;
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
      
       public UserSession GetUserSession()
       {
            string json = HttpContext.Session.GetString(EduConstant.UserSessionKey);

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<UserSession>(json);
            else
            {
                HttpContext.Response.Redirect("/User/Login");

            }
            return null;
             

       }

        public void SetUserSesion(string openId)
        {
            UserSession session = new UserSession()
            {
                OpenId = openId
            };
            var json = JsonConvert.SerializeObject(session);
            HttpContext.Session.SetString(EduConstant.UserSessionKey, json);
        }
    }
}
