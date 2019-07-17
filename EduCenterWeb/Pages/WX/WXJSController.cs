using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.WX;
using EduCenterModel.WX;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WxPayAPI;

namespace EduCenterWeb.Pages.WX
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WXJSController : EduBaseApi
    {
        [HttpPost]
        public WxJsAPIEntity InitConfig(string url)
        {
           
            try
            {
                var us = GetUserSession(false);
                if(us !=null)
                {
                    WxJsAPIEntity result = WXApi.eduGetJsConfig(url);
                    result.openId = us.OpenId;
                    return result;
                }
                else
                {
                    WxJsAPIEntity result = new WxJsAPIEntity
                    {
                        ErrorMsg = "请重新登陆",
                        IntMsg = -1
                    };
                    return result;
                }
             
            }
            catch(Exception ex)
            {
                WxJsAPIEntity result = new WxJsAPIEntity
                {
                    ErrorMsg = ex.Message
                };
                return result;
            }
        //    return null;
           
           

        }
    }
}