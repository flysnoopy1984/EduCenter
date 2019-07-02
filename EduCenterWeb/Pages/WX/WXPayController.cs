using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.Session;
using EduCenterModel.WX;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WxPayAPI;

namespace EduCenterWeb.Pages.WX
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WXPayController : ControllerBase
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

        [HttpPost]
        public WxPayOrder Pay(WxPayInfo wxPayInfo)
        {
            var us = GetUserSession(false);
            var result = new WxPayOrder
            {
                appId = "111"
            };

            return result;

            WxPayOrder wxOrder = null;
            JsApiPay jsApiPay = new JsApiPay();
            try
            {
                string notifyUrl = "http://edu.iqiban.cn/api/wxPay/Notify";

                jsApiPay.openid = wxPayInfo.OpenId;
                jsApiPay.total_fee = (int)wxPayInfo.PayAmount * 100;

                string OrderNo = WxPayApi.GenerateOutTradeNo();

                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(wxPayInfo.ItemDes, notifyUrl, OrderNo);
                WxPayData wxJsApiParam = jsApiPay.GetJsApiParameters();

                wxOrder = new WxPayOrder()
                {
                    appId = wxJsApiParam.GetValue("appId").ToString(),
                    nonceStr = wxJsApiParam.GetValue("nonceStr").ToString(),
                    package = wxJsApiParam.GetValue("package").ToString(),
                    paySign = wxJsApiParam.GetValue("paySign").ToString(),
                    signType = "MD5",
                    timeStamp = wxJsApiParam.GetValue("timeStamp").ToString(),
                    OrderNo = OrderNo,

                };
            }
            catch(Exception ex)
            {
                NLogHelper.ErrorTxt($"WXPayController Pay:{ex.Message}");
            }
          
            return wxOrder;
        }

        [HttpPost]
        public void Notify()
        {
            NLogHelper.InfoTxt("==============WXPayNotify=================");

          
        }

        public void Test()
        {
            NLogHelper.InfoTxt("==============WXPayNotify=================");


        }
    }
}