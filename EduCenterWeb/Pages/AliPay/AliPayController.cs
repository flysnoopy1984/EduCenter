using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alipay.AopSdk.Core;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;
using EduCenterCore.EduFramework;
using EduCenterModel.AliPay;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduCenterWeb.Pages.AliPay
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AliPayController : EduBaseApi
    {
        private AliPaySrv _AliPaySrv;
      
        public AliPayController(AliPaySrv aliPaySrv)
        {
            _AliPaySrv = aliPaySrv;
        }

        /// <summary>
        /// 转账
        /// </summary>
        /// <param name="wxPayInfo"></param>
        [HttpPost]
        public AlipayFundTransToaccountTransferResponse TransferAmount(string toUserAliPayAccount, string userOpenId,double amount)
        {
            return _AliPaySrv.TransferAmount(toUserAliPayAccount, userOpenId, amount);
        }
    }
}