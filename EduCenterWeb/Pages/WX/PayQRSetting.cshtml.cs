using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterModel.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WX
{
    public class PayQRSettingModel : EduBaseAppPageModel
    {
        public int UserRole { get; set; }
        public void OnGet()
        {
            var us = GetUserSession();
            if(us!=null)
            {
                UserRole = (int)us.UserRole;
            }
        }

        public IActionResult OnPostQRGen(double payAmount,int courseTime = 0)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var url = $"{Request.Scheme}://{Request.Host}/WX/PayQRMoney?amt={payAmount}&ct={courseTime}";
                var fileName = $"{payAmount}_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.png";
                var savePath = EduEnviroment.DicPath_QRPay + fileName;
                List<string> desc = new List<string>();
                desc.Add($"请用户扫码付款,支付金额【{payAmount}】元");
                if(courseTime>0)
                    desc.Add($"共计: 【{courseTime}】节课时");
                QRHelper.GenQR(url, savePath,desc);
                result.SuccessMsg = EduEnviroment.VirPath_QRPay + fileName;
             
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "生成失败：" + ex.Message;
                NLogHelper.ErrorTxt($"二维码生成失败:{ex.Message}");
            }
            return new JsonResult(result);
        }
    }
}