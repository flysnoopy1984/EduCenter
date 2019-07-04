using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
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

        public LoginModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
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
            }

            if (!EduConfig.IsTest)
                LoginWX();

        }

       // [EnableCors("any")]
        public IActionResult OnPostUserLogin()
        {
            ResultNormal result = new ResultNormal();
            try
            {

                if (!EduConfig.IsTest)
                {
                    var us = GetUserSession(false);
                    if (us == null)
                        result.ErrorMsg = "登陆失败,请联系客服";
                    
                }
                else
                {
                    var ui = _UserSrv.GetUserInfo("oh6cV1QhPLj6XPesheYUQ4XtuGTs");
                    WXLoginCallBack(ui);
                }
                //if (result.IsSuccess)
                //    result.IntMsg = 10;



            }
            catch (Exception ex)
            {
                result.ErrorMsg = "登陆失败";

                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

        private void LoginWX()
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["code"]))
            {
                //获取code码，以获取openid和access_token
                string code = HttpContext.Request.Query["code"];
                var accessToken = WXApi.GetOAuth2AccessTokenFromCode(code);
                if (!string.IsNullOrEmpty(accessToken.openid))
                {
                    string url_userInfo = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", accessToken.access_token, accessToken.openid);
                    WXUserInfo wxUser = HttpHelper.Get<WXUserInfo>(url_userInfo);
                    EUserInfo ui = _UserSrv.AddOrUpdateFromWXUser(wxUser);
                    WXLoginCallBack(ui);
                }
            }
            else
            {
                try
                {
                    var redirect_uri = System.Web.HttpUtility.UrlEncode("http://edu.iqianba.cn/User/Login", System.Text.Encoding.UTF8);
                    WxPayData data = new WxPayData();
                    data.SetValue("appid", WxConfig.APPID);
                    data.SetValue("redirect_uri", redirect_uri);
                    data.SetValue("response_type", "code");
                    data.SetValue("scope", "snsapi_userinfo");

                    data.SetValue("state", "1" + "#wechat_redirect");
                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();
                  //  HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    NLogHelper.InfoTxt($"LoginWX 请求:{url}");
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

            CourseScheduleType courseScheduleType = _UserSrv.GetCurrentCourseScheduleType(ui.OpenId);

            base.SetUserSesion(ui.OpenId, ui.Name, ui.wx_headimgurl, ui.Phone, courseScheduleType, ui.UserRole, ui.MemberType, userAccount);
        }
    }
}