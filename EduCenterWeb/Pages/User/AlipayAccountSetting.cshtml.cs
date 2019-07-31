using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class AlipayAccountSettingModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;
        public string AliPayAccount { get; set; }
        public AlipayAccountSettingModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
           
        }

        public void OnGet()
        {
            AliPayAccount = "";
            var us = base.GetUserSession();
            if(us!=null)
            {
                var ac = _UserSrv.GetUserAccount(us.OpenId);
                if (ac != null)
                {
                    AliPayAccount = ac.AliPayAccount;
                }
            }
         
        }

        public IActionResult OnPostSaveAccount(string AliPayAccount)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    _UserSrv.UpdateAlipayAccount(us.OpenId, AliPayAccount);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "设置支付宝账户失败，请联系客服";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }
    }
}