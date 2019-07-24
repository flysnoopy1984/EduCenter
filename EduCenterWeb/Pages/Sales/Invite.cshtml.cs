using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.WX;
using EduCenterModel.Common;
using EduCenterModel.QR;
using EduCenterModel.Sales.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Sales
{
    public class InviteModel : EduBaseAppPageModel
    {
        private SalesSrv _SalesSrv;
        private UserSrv _UserSrv;
        public EQRInvite QRInvite { get; set; }

        public List<RInviteLog> InviteLog { get; set; }
        public InviteModel(SalesSrv salesSrv, UserSrv userSrv)
        {
            _SalesSrv = salesSrv;
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            var us = GetUserSession();
            if (us != null)
            {
                QRInvite = _SalesSrv.GetQRInvite(us.OpenId);

                if (QRInvite!=null)
                {
                    int totalPages;
                    InviteLog = _SalesSrv.QueryInviteLog(us.OpenId, out totalPages, 1, 5);

                 //   InitWxConfig();
                }
                 
                
            }
                
        }

        //private void InitWxConfig()
        //{
        //    var url = HttpContext.Request.Host+"/Sales/Invite";
        //    var p = WXApi.eduGetJsConfig(url);

        //}

     

        public IActionResult OnPostGenerateQR()
        {
            ResultNormal result = new ResultNormal();
            var us = GetUserSession(false);
            try
            {
                if(us !=null)
                {
                    if(string.IsNullOrEmpty(us.Phone))
                    {
                        result.IntMsg = -2;
                        result.ErrorMsg = "请先绑定您的手机号";
                        return new JsonResult(result);
                    }
                    else
                        _SalesSrv.GenQRInvite(us.OpenId, us.Phone,us.HeaderUrl);
                }
                
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆后再尝试";
                }
            }
            catch(EduException eex)
            {
                result.ErrorMsg = eex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "生成失败！";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }
    }
}