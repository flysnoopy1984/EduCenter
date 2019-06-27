using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterModel.Order;
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
        public EUserAccount UserAccount { get; set; }
        public MyCourseTimeModel(UserSrv userSrv, OrderSrv orderSrv)
        {
            _OrderSrv = orderSrv;
            _UserSrv = userSrv;
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
    }
}