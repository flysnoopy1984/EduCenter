
using EduCenterCore.EduFramework;
using EduCenterModel.BaseEnum;
using EduCenterModel.Session;
using EduCenterModel.User;
using EduCenterSrv.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduCenterWeb.Pages
{
    public class EduBasePageModel: PageModel
    {
        public BackendSession GetBackendSession(bool toLoginIfError = true)
        {
            string json = HttpContext.Session.GetString(EduConstant.BackendSessionKey);

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<BackendSession>(json);
            else
            {
                if (toLoginIfError)
                    HttpContext.Response.Redirect("/WebBackend/Login");

            }
            return null;

        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
           
            base.OnPageHandlerExecuting(context);
            string json = HttpContext.Session.GetString(EduConstant.BackendSessionKey);
            if (string.IsNullOrEmpty(json))
            {
                context.HttpContext.Response.Redirect("/WebBackend/Login");
            }
            else
            {
                var session = JsonConvert.DeserializeObject<BackendSession>(json);
                if((int)session.UserRole <30)
                    context.HttpContext.Response.Redirect("/WebBackend/Login");
            }
        }
    }
}
