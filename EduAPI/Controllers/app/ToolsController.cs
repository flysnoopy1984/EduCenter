using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterSrv;
using EduCenterSrv.SMS;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduAPI.Controllers.app
{
    [Route("api/[controller]/[action]")]
    public class ToolsController : BaseAPI
    {
        private SMSSrv _smsSrv;
        private UserSrv _userSrv;
        public ToolsController(SMSSrv smsSrv, UserSrv userSrv)
        {
            _smsSrv = smsSrv;
            _userSrv = userSrv;
        }

        [HttpPost]
        public ResultObject<OutSMS> SmsGetCode(string mobilePhone,string userOpenId, int IntervalSec)
        {
            ResultObject<OutSMS> result = new ResultObject<OutSMS>();
            try
            {
                bool isUser = _userSrv.IsExistUser(userOpenId);
                if(isUser)
                    result.Entity = _smsSrv.RequireVerifyCode(mobilePhone, IntervalSec);
                else
                {
                    result.ErrorMsg = "非法请求";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "请求短信失败";
                NLogHelper.ErrorTxt($"验证码获取[GetSmsCode]:{ex.Message}");
            }

            return result;
        }

        [HttpPost]
        public ResultObject<OutSMS> SmsVerifyCode(string mobilePhone, string Code)
        {
            ResultObject<OutSMS> result = new ResultObject<OutSMS>();
            try
            {
                result.Entity = _smsSrv.SubmitUserVerifyCode(mobilePhone, Code);
               
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "系统错误。请联系客服！";
                NLogHelper.ErrorTxt($"验证校验码[SmsVerifyCode]:{ex.Message}");
            }
            return result;

        }

    }
}
