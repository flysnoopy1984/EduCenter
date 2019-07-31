using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterModel.Order;
using EduCenterModel.Order.Result;
using EduCenterModel.Pages.User;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyCourseTimeModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;
        private OrderSrv _OrderSrv;
        private AliPaySrv _AliPaySrv;
        private SalesSrv _SalesSrv;
        public EUserAccount UserAccount { get; set; }
        public MyCourseTimeModel(UserSrv userSrv, OrderSrv orderSrv, AliPaySrv aliPaySrv,SalesSrv salesSrv)
        {
            _OrderSrv = orderSrv;
            _UserSrv = userSrv;
            _AliPaySrv = aliPaySrv;
            _SalesSrv = salesSrv;
        }
        public void OnGet()
        {
            var us = base.GetUserSession();
            if (us != null)
            {
                UserAccount = _UserSrv.GetUserAccount(us.OpenId);
            }
        }

        public IActionResult OnPostQueryReChargeList(int maxLine=10)
        {
            ResultList<RUserCharge> result = new ResultList<RUserCharge>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    result.List = _OrderSrv.QueryChargeOrderList(us.OpenId, 1, 10);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "没有获取数据";
                NLogHelper.ErrorTxt(ex.Message);

            }
            return new JsonResult(result);
        }

        public IActionResult OnPostCheckUserAccount()
        {
            ResultObject<EUserAccount> result = new ResultObject<EUserAccount>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    var userAccount = _UserSrv.GetUserAccount(us.OpenId);
                    if (string.IsNullOrEmpty(userAccount.AliPayAccount))
                    {
                        result.IntMsg = -2;
                        result.ErrorMsg = "请设置转账支付宝账户";
                    }
                    else
                        result.Entity = userAccount;
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "没有获取数据";
                NLogHelper.ErrorTxt(ex.Message);

            }
            return new JsonResult(result);
        }

        public IActionResult OnPostQueryConsumeList(int maxLine = 20)
        {
            ResultList<RUserComsume> result = new ResultList<RUserComsume>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    result.List = _UserSrv.QueryUserCourseComsume(us.OpenId, 1, 10);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "没有获取数据";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostTransferAmount()
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    var ac =  _UserSrv.GetUserAccount(us.OpenId);
                    if(ac.RemainRewards>0)
                    {
                        var r = _AliPaySrv.TransferAmount(ac.AliPayAccount, ac.UserOpenId, ac.RemainRewards);
                        if(!r.IsError)
                        {
                            _SalesSrv.CreateTransfer(ac.RemainRewards, ac.UserOpenId, r.OutBizNo,false);
                            ac.RemainRewards = 0;
                            _SalesSrv.SaveChanges();
                        }
                        else
                        {
                            result.ErrorMsg = r.Msg;
                        }
                    }
                    else
                    {
                        result.IntMsg = -2;
                        result.ErrorMsg = "余额不足不能提取！";
                    }
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "没有获取数据";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostQueryAmountList(int maxLine = 20)
        {
            ResultList<RAmountTrans> result = new ResultList<RAmountTrans>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    result.List = _SalesSrv.QueryUserAmountTrans(us.OpenId);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "没有获取数据";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }
    }
}