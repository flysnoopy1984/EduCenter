using EduCenterCore.EduFramework;
using EduCenterModel.BaseEnum;
using EduCenterModel.Session;
using EduCenterModel.User;
using EduCenterSrv;
using EduCenterSrv.Common;
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
    public class EduBaseAppPageModel:PageModel
    {
        
      
        public string jsVersion
        {
            get { return EduConfig.Version; }
        }

        public void ClearUserSession()
        {
            HttpContext.Session.Remove(EduConstant.UserSessionKey);
        }

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
        private bool IsCourseStartDate(UserSession us)
        {
            DateTime buyDate = DateTime.MinValue;
            if (us.CurrentScheduleType == CourseScheduleType.Summer)
                buyDate = us.UserAccount.SummerBuyDate;
            else if (us.CurrentScheduleType == CourseScheduleType.Winter)
                buyDate = us.UserAccount.WinterBuyDate;
            else
                buyDate = us.UserAccount.BuyDate;

            return buyDate.Date == DateTime.Today;


        }

        public UserSession SetUserSesion(string openId,string userName,
            string headerUrl,string phone, 
            CourseScheduleType currentScheduleType,
            UserRole userRole,
            MemberType memberType,
            bool courseSkipToday,
            EUserAccount userAccount)
        {
            UserSession session = new UserSession()
            {
                OpenId = openId,
                UserName = userName,
                HeaderUrl = headerUrl,
                Phone = phone,
                CurrentScheduleType = currentScheduleType,
                CurrentScheduleTypeName = BaseEnumSrv.GetCourseScheduleTypeName(currentScheduleType),
                MemeberType = memberType,
                UserRole = userRole,
                CourseSkipToday = courseSkipToday,
                UserAccount = userAccount
            };
           
            var json = JsonConvert.SerializeObject(session);
            HttpContext.Session.SetString(EduConstant.UserSessionKey, json);

            return session;
        }
        public void SetUserSesion(UserSession session)
        {
         
            var json = JsonConvert.SerializeObject(session);
            HttpContext.Session.SetString(EduConstant.UserSessionKey, json);
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);
            var vs = GetUserSession(false);
            if(vs !=null)
            {
                if(vs.UserRole == UserRole.BlackList)
                {
                    var msg = System.Web.HttpUtility.UrlEncode("您没有权限，请到店联系工作人员!");
                    context.HttpContext.Response.Redirect($"/Common/ErrorMessage?msg={msg}");
                }
            }
            //string json = HttpContext.Session.GetString(EduConstant.BackendSessionKey);
            //if (string.IsNullOrEmpty(json))
            //{
            //    context.HttpContext.Response.Redirect("/WebBackend/Login");
            //}
            //else
            //{
            //    var session = JsonConvert.DeserializeObject<BackendSession>(json);
            //    if ((int)session.UserRole < 30)
            //        context.HttpContext.Response.Redirect("/WebBackend/Login");
            //}

            //var us = GetBackendSession(false);
            //if (us != null)
            //{
            //    this.ViewData["UserRole"] = (int)us.UserRole;
            //}
        }




    }
}
