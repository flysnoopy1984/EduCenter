using System;
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
          
            string act = HttpContext.Request.Query["act"];
            if(!string.IsNullOrEmpty(act))
            {
                if(act == "refresh")
                {
                    ClearUserSession();
                }
                //else if(act == "Invite")
                //{
                //    string ownOpenId = HttpContext.Request.Query["OwnOpenId"];
                //    if(!string.IsNullOrEmpty(ownOpenId))
                //    {
                //      //  WXApi.getAccessToken();
                //    }
                //}
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
            //if (!string.IsNullOrEmpty(HttpContext.Request.Query["code"]))
            //{
            //    string code = HttpContext.Request.Query["code"];
            //    var accessToken = WXApi.GetOAuth2AccessTokenFromCode(code);

            //    if (!string.IsNullOrEmpty(accessToken.openid))
            //    {
            //        var ui = _UserSrv.GetUserInfo(accessToken.openid);

            //        if (ui != null)
            //        {
            //            WXLoginCallBack(ui);
            //            if (!string.IsNullOrEmpty(toPage))
            //            {
            //                HttpContext.Response.Redirect(toPage);
            //            }
            //        }
            //        else
            //        {
            //            HttpContext.Response.Redirect("/User/Login");
            //        }
            //    }    
            //}
            //else
            //{
            //    var redirect_uri = System.Web.HttpUtility.UrlEncode($"http://edu.iqianba.cn/User/Login?handler=LoginTransfer2&toPage={toPage}", System.Text.Encoding.UTF8);
            //    WxPayData data = new WxPayData();
            //    data.SetValue("appid", WxConfig.APPID);
            //    data.SetValue("redirect_uri", redirect_uri);
            //    data.SetValue("response_type", "code");
            //    data.SetValue("scope", "snsapi_base");
            //    data.SetValue("state", "1" + "#wechat_redirect");
            //    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();

            //    HttpContext.Response.Redirect(url);
            //}

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
                    //oh6cV1ZcN2GzbZpaYELK8Uv3a2rU
                    //oh6cV1QhPLj6XPesheYUQ4XtuGTs jacky
                    var ui = _UserSrv.GetUserInfo("oh6cV1djEwlU7Hup1KynlnyFrmdA");
                    WXLoginCallBack(ui);
                    userSession = GetUserSession(false);
                }
              
                if (result.IsSuccess)
                {
                    if (userSession.UserRole == UserRole.Teacher)
                    {
                        var tec = _TecSrv.GetByOpenId(userSession.OpenId);
                        if(tec!=null)
                        {
                            userSession.TecCode = tec.Code;
                            SetUserSesion(userSession);
                            result.IntMsg = 10;

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

        private void LoginWX()
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["code"]))
            {
                //获取code码，以获取openid和access_token
                string code = HttpContext.Request.Query["code"];
            //    NLogHelper.InfoTxt($"LoginWX-Query:{HttpContext.Request.QueryString}");
                var accessToken = WXApi.GetOAuth2AccessTokenFromCode(code);
                if (!string.IsNullOrEmpty(accessToken.openid))
                {
                    string url_userInfo = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", accessToken.access_token, accessToken.openid);
                    WXUserInfo wxUser = HttpHelper.Get<WXUserInfo>(url_userInfo);

                    EUserInfo ui = TryInvitedUserComing(accessToken.openid, wxUser);
                   if (ui== null)
                   {
                        ui = _UserSrv.AddOrUpdateFromWXUser(wxUser);
                   }

                    WXLoginCallBack(ui);

                    string toPage = HttpContext.Request.Query["toPage"];
                    if (!string.IsNullOrEmpty(toPage))
                    {
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
                    var reUrl = $"http://edu.iqianba.cn/User/Login{Request.QueryString}";
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