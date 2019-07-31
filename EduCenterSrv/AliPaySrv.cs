using Alipay.AopSdk.Core;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;
using EduCenterCore.EduFramework;
using EduCenterModel.AliPay;
using EduCenterSrv.Common;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv
{
    public class AliPaySrv: BaseSrv
    {
        public AliPaySrv(EduDbContext dbContext) : base(dbContext)
        {

        }

       public AlipayFundTransToaccountTransferResponse TransferAmount(string toUserAliPayAccount, string userOpenId, double amount)
        {
            EAliPayApplication app = StaticDataSrv.GetAliPayApplication();
            IAopClient aliyapClient = new DefaultAopClient("https://openapi.alipay.com/gateway.do", app.AppId,
                 app.Merchant_Private_Key, "json", "1.0", "RSA2", app.Merchant_Public_key, "GBK", false);

            AlipayFundTransToaccountTransferRequest request = new AlipayFundTransToaccountTransferRequest();

            var TransferId = EduCodeGenerator.TransferOrderNo();

            AlipayFundTransToaccountTransferModel model = new AlipayFundTransToaccountTransferModel();
            model.Amount = amount.ToString("0.00");
            model.OutBizNo = TransferId;
            model.PayeeType = "ALIPAY_LOGONID";
            model.PayeeAccount = toUserAliPayAccount;
            model.PayerShowName = "云艺书院奖励金";
            request.SetBizModel(model);
            AlipayFundTransToaccountTransferResponse response = aliyapClient.Execute(request);

          

            return response;

            //if (PayTargetMode == PayTargetMode.AliPayAccount)
            //    model.PayeeType = "ALIPAY_LOGONID";
            //else
            //    model.PayeeType = "ALIPAY_USERID";

            //model.PayeeAccount = toAliPayAccount;
            //if (!string.IsNullOrEmpty(ShowName))
            //{
            //    model.PayerShowName = ShowName;
            //}
            //else
            //{
            //    string profix = "";
            //    if (target == TransferTarget.ParentAgent)
            //        profix = "(上级佣金)";
            //    else if (target == TransferTarget.Agent)
            //        profix = "(代理费)";
            //    else if (target == TransferTarget.User)
            //        profix = "(打款)";
            //    else if (target == TransferTarget.L3Agent)
            //        profix = "(三级)";
            //    else if (target == TransferTarget.MidStore)
            //        profix = "(码商)";
            //    model.PayerShowName = profix + "服务费";
            //}

            //if (order != null)
            //    model.Remark = string.Format("#{0}-订单金额：{1}-订单ID：{2}", order.AgentName, order.TotalAmount, order.OrderNo);

            //request.SetBizModel(model);

            //AlipayFundTransToaccountTransferResponse response = aliyapClient.Execute(request);

            //return response;
        }
    }
}
