using EduCenterCore.EduFramework;
using EduCenterModel.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EduCenterWeb.Pages
{
    public class EduBaseApi: ControllerBase
    {
        public UserSession GetUserSession(bool toLoginIfError = true)
        {
            string json = HttpContext.Session.GetString(EduConstant.UserSessionKey);

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<UserSession>(json);
            else
            {
                if (toLoginIfError)
                    HttpContext.Response.Redirect("/User/Login");

            }
            return null;

        }

        public void SetUserSesion(UserSession session)
        {

            var json = JsonConvert.SerializeObject(session);
            HttpContext.Session.SetString(EduConstant.UserSessionKey, json);
        }

    }
}
