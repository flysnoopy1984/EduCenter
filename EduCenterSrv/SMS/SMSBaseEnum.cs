using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv.SMS
{
    public enum SMSTemplate
    {
        //验证码：您的验证码为:{1}. 支付成功后。为保证商家发货给您，请注意确认短信。
        VerifyCode = 60627,
        //收款确认：您已支付成功，您的收款确认码为：{1}。若代理商家已打款，请到以下地址进行收款确认：http://b.iqianba.cn/。
        ReceiveConfirm = 60977,

        //您的验证码为{1}，请于{2}分钟内填写。如非本人操作，请忽略短信
        NormalVerify = 65091,
    }

    public enum SMSVerifyStatus
    {
        //基本不用。。。
        Verifying = 1,
        //已发送
        Sent = 2,
        //发送失败
        SentFailure = 6,

        //校验成功
        Success = 3,
        //校验失败
        Failure = 4,

        //过期
        Expired = 5,
        UnKnown = -1,
    }

    public enum SMSEvent
    {
        UserPhoneVerify = 0,
    }
}
