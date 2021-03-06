﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Session;
using EduCenterModel.User;
using EduCenterModel.WX;
using EduCenterSrv;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    
    public class LoginModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;
        private TecSrv _TecSrv;
        private BusinessSrv _BusinessSrv;
        public LoginModel(UserSrv userSrv, TecSrv tecSrv,BusinessSrv businessSrv)
        {
            _UserSrv = userSrv;
            _TecSrv = tecSrv;
            _BusinessSrv = businessSrv;
        }
        public void OnGet()
        {
            if(HttpContext.Request.Host.Host == "www.yunyishuyuan.cn")
            {
                HttpContext.Response.Redirect("/WebBackend/Login");
                return;
            }

            string act = HttpContext.Request.Query["act"];
            if(!string.IsNullOrEmpty(act))
            {
                if(act == "refresh")
                {
                    ClearUserSession();
                }
            }

            if (!EduConfig.IsTest)
                LoginWX();

            
        }

        /// <summary>
        /// 自定登陆，并跳转
        /// WX试听课提醒，
        /// WX奖励金提醒
        /// 直接跳转
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="toPage"></param>
        public void OnGetLoginTransfer(string openId,string toPage)
        {
           
            var ui = _UserSrv.GetUserInfo(openId);
            if(ui!=null)
            {
                WXLoginCallBack(ui);
                if(!string.IsNullOrEmpty(toPage))
                {
                    HttpContext.Response.Redirect(toPage);
                }
            }
            else
            {
                HttpContext.Response.Redirect("/User/Login");
            }
        }

        public void OnGetLoginTransfer2()
        {
            LoginWX();
        }

        /// <summary>
        /// 页面登陆按钮点击
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostUserLogin()
        {
            ResultNormal result = new ResultNormal();
            UserSession userSession = null;
            try
            {

                if (!EduConfig.IsTest)
                {
                    userSession = GetUserSession(false);
                    if (userSession == null)
                        result.ErrorMsg = "登陆失败,请联系客服";
                }
                else
                {
                    //oh6cV1dh0hjoGEizCoKH1KU70UwQ 童老师
                    //oh6cV1UUH2cg1p3--SPVnJdDpgbM 电子商务
                    //oh6cV1QhPLj6XPesheYUQ4XtuGTs jacky
                    var ui = _UserSrv.GetUserInfo("oh6cV1QhPLj6XPesheYUQ4XtuGTs");
                    WXLoginCallBack(ui);
                    userSession = GetUserSession(false);
                    result.IntMsg = (int)ui.UserRole;
                }
              
                if (result.IsSuccess)
                {
                    if (userSession.UserRole == UserRole.Teacher)
                    {
                        NLogHelper.InfoTxt($"User OpenId:{userSession.OpenId}");
                       
                        var tec = _TecSrv.GetByOpenId(userSession.OpenId);
                        NLogHelper.InfoTxt($"TecCode:{tec.Code}");
                        if (tec!=null)
                        {
                            userSession.TecCode = tec.Code;
                            SetUserSesion(userSession);
                            result.IntMsg = (int)UserRole.Teacher;

                        }
                    }
                }
 

            }
            catch (Exception ex)
            {
                result.ErrorMsg = "登陆失败";

                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

     
        private EUserInfo TryInvitedUserComing(string OpenId, WXUserInfo wXUser)
        {
            var act = HttpContext.Request.Query["act"];
            EUserInfo ui = null;
            if (act == "Invite")
            {
                string ownOpenId = HttpContext.Request.Query["OwnOpenId"];
                if(!string.IsNullOrEmpty(ownOpenId))
                {
                    ui = _BusinessSrv.InvitedUserComing(OpenId, ownOpenId, wXUser);
                   
                }
            }
            return ui;
        }

        public void LoginWX()
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["code"]))
            {
                //获取code码，以获取openid和access_token
                string code = HttpContext.Request.Query["code"];
               NLogHelper.InfoTxt($"LoginWX-Query:{HttpContext.Request.QueryString}");
                var accessToken = WXApi.GetOAuth2AccessTokenFromCode(code);
                if (!string.IsNullOrEmpty(accessToken.openid))
                {
                    string url_userInfo = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", accessToken.access_token, accessToken.openid);
                    WXUserInfo wxUser = HttpHelper.Get<WXUserInfo>(url_userInfo,false);

                   EUserInfo ui = TryInvitedUserComing(accessToken.openid, wxUser);
                   if (ui== null)
                   {
                        ui = _UserSrv.AddOrUpdateFromWXUser(wxUser);
                   }
                   

                    WXLoginCallBack(ui);

                    string toPage = HttpContext.Request.Query["toPage"];
                    if (!string.IsNullOrEmpty(toPage))
                    {
                        if(toPage.Contains("/User/MyCourse") && ui.UserRole == UserRole.Teacher)
                        {
                          //  HttpContext.Response.Redirect("/Teacher/DayCourse");
                            return;
                        }
                        //微信QR支付页面
                        string amt = HttpContext.Request.Query["amt"];
                        if(!string.IsNullOrEmpty(amt))
                        {
                            var ct = HttpContext.Request.Query["ct"];
                            toPage += $"?amt={amt}&ct={ct}";
                        }  
                        HttpContext.Response.Redirect(toPage);
                    }                  
                }
            }
            else
            {
                try
                {
                    var reUrl = $"https://edu.iqianba.cn/User/Login{Request.QueryString}";
                 //   NLogHelper.InfoTxt($"Login-reUrl:{reUrl}");
                    var redirect_uri = System.Web.HttpUtility.UrlEncode(reUrl, System.Text.Encoding.UTF8);
                    WxPayData data = new WxPayData();
                    data.SetValue("appid", WxConfig.APPID);
                    data.SetValue("redirect_uri", redirect_uri);
                    data.SetValue("response_type", "code");
                    data.SetValue("scope", "snsapi_userinfo");
                    data.SetValue("state", "1" + "#wechat_redirect");
                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();
             
                    HttpContext.Response.Redirect(url);
                }
                catch(Exception ex)
                {
                    NLogHelper.ErrorTxt($"LoginWX:{ex.Message}");
                }
           
                return;
            }
        }

        private void WXLoginCallBack(EUserInfo ui)
        {
           var userAccount = _UserSrv.GetUserAccount(ui.OpenId);

            CourseScheduleType courseScheduleType = _UserSrv.GetCurrentCourseScheduleType(ui.OpenId,ui.MemberType);

            bool isSkipTodayCourse = _UserSrv.IsSkipTodayUserCourse(ui.OpenId);

            base.SetUserSesion(ui.OpenId, ui.Name, ui.wx_headimgurl, ui.Phone, courseScheduleType, ui.UserRole, ui.MemberType, isSkipTodayCourse, userAccount);

           
        }
    }
}