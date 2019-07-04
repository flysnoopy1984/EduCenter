using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.Common;
using EduCenterModel.Session;
using EduCenterModel.User;
using EduCenterModel.WX;
using EduCenterSrv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WxPayAPI;

namespace EduCenterWeb.Pages.WX
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WXPayController : EduBaseApi
    {
        private BusinessSrv _BusinessSrv;
        private CourseSrv _CourseSrv;
        public WXPayController(BusinessSrv businessSrv, CourseSrv courseSrv)
        {
            _BusinessSrv = businessSrv;
            _CourseSrv = courseSrv;
        }
      

        [HttpPost]
        public WxPayOrder Pay(WxPayInfo wxPayInfo)
        {
            //  var us = GetUserSession(false);


            WxPayOrder wxOrder = null;
            JsApiPay jsApiPay = new JsApiPay();
            try
            {
                var us = GetUserSession(false);
                if (us != null)
                {
                    string notifyUrl = "http://edu.iqiban.cn/api/wxPay/Notify";

                    var eCoursePrice = _CourseSrv.GetCoursePrice(wxPayInfo.PriceCode);
                    if (eCoursePrice.CourseScheduleType == EduCenterModel.BaseEnum.CourseScheduleType.VIP)
                    {
                        eCoursePrice.Qty = wxPayInfo.VIPQty;
                        eCoursePrice.Price = us.UserAccount.VIPPrice1 * eCoursePrice.Qty;
                    }


                    jsApiPay.openid = us.OpenId;
                    jsApiPay.total_fee = (int)eCoursePrice.Price * 100;

                    //jsApiPay.total_fee = 1;

                    string OrderNo = WxPayApi.GenerateOutTradeNo();
                    var order = _BusinessSrv.PayCourseOrder(us.OpenId, eCoursePrice);

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
                        EduOrderNo = order.OrderId

                    };
                  
                   
                }
                else
                {
                    wxOrder = new WxPayOrder()
                    {
                        IntMsg = -1,
                        IsSuccess = false,
                        ErrorMsg = "请重新登陆",
                    };
                }

            }
            catch (Exception ex)
            {
                NLogHelper.ErrorTxt($"WXPayController Pay:{ex.Message}");
                wxOrder = new WxPayOrder()
                {
                    IsSuccess = false,
                    ErrorMsg = "支付失败，请联系客服",
                };
            }
          
            return wxOrder;
        }

        [HttpPost]
        public ResultNormal PaySuccess(WXPaySuccess paySuccess)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                EUserAccount eUserAccount = _BusinessSrv.PayCourseSuccess(paySuccess.OrderId);
                if (eUserAccount != null)
                {
                    var us = GetUserSession(false);
                    us.UserAccount = eUserAccount;
                    us.UserRole = EduCenterModel.BaseEnum.UserRole.Member;
                    SetUserSesion(us);
                }
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return result;
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