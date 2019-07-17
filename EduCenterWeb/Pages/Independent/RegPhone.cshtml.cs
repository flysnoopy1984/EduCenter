using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterSrv;
using EduCenterSrv.SMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Independent
{
    public class RegPhoneModel : EduBaseAppPageModel
    {
        private SMSSrv _smsSrv;
        private BusinessSrv _BusinessSrv;
        public RegPhoneModel(SMSSrv smsSrv, BusinessSrv businessSrv)
        {
            _smsSrv = smsSrv;
            _BusinessSrv = businessSrv;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPostRequireVerifyCode(string mobilePhone,int IntervalSec)
        {
            

            ResultObject<OutSMS> result = new ResultObject<OutSMS>();
            try
            {
                var us = GetUserSession(false);
                if(us != null)
                    result.Entity = _smsSrv.RequireVerifyCode(mobilePhone, IntervalSec);
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆";
                }
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "请求短信失败";
                NLogHelper.ErrorTxt($"验证码获取[OnPostRequireVerifyCode]:{ex.Message}");
            }
            return new JsonResult(result);
            
        }

        public IActionResult OnPostSubmitVerifyCode(string mobilePhone,string Code,string BabyName)
        {
            ResultObject<OutSMS> result = new ResultObject<OutSMS>();
            try
            {
                result.Entity = _smsSrv.SubmitUserVerifyCode(mobilePhone, Code);
                if(result.Entity.SMSVerifyStatus == SMSVerifyStatus.Success)
                {
                    var us = GetUserSession(false);
                    DoUpdateUserSimpleInfo(us.OpenId,mobilePhone, BabyName);
                }
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "系统错误。请联系客服！";
                NLogHelper.ErrorTxt($"验证校验码[OnPostSubmitVerifyCode]:{ex.Message}");
            }
            return new JsonResult(result);
            
        }

        /// <summary>
        /// 注册时更新用户孩子姓名
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="phone"></param>
        /// <param name="BabyName"></param>
        private void DoUpdateUserSimpleInfo(string openId,string phone,string BabyName)
        {
           
            _BusinessSrv.UserRegisterByPhone(openId, phone, BabyName);

            var us = GetUserSession(false);
            us.Phone = phone;
            SetUserSesion(us);
        }
    }
}