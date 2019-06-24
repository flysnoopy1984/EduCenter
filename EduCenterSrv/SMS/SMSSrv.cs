using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EduCenterSrv.SMS
{
    public class SMSSrv: BaseSrv
    {
        public SMSSrv(EduDbContext dbContext) : base(dbContext)
        {

        }

        private const int SMSMaxIntervalSec = 600;

        private string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;

            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }

        public OutSMS SubmitUserVerifyCode(string mobilePhone, string Code)
        {

            OutSMS OutSMS = new OutSMS();
            OutSMS.SMSVerifyStatus = SMSVerifyStatus.UnKnown;


            var sms = _dbContext.DBSMSVerification.Where(s => s.MobilePhone == mobilePhone
                                                && (
                                                   s.SMSVerifyStatus == SMSVerifyStatus.Sent ||
                                                   s.SMSVerifyStatus == SMSVerifyStatus.Verifying ||
                                                   s.SMSVerifyStatus == SMSVerifyStatus.Failure
                                                   )
                                                )
                   .OrderByDescending(s => s.ID)
                   .FirstOrDefault();
            if (sms == null)
            {
                OutSMS.SMSVerifyStatus = SMSVerifyStatus.UnKnown;
            }
            else
            {
                TimeSpan nowtimespan = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan endtimespan = new TimeSpan(sms.SendDateTime.Ticks);
                TimeSpan timespan = nowtimespan.Subtract(endtimespan).Duration();
                int CurSec = Convert.ToInt32(timespan.TotalSeconds);
                if (CurSec > SMSMaxIntervalSec)
                {
                    OutSMS.SMSVerifyStatus = SMSVerifyStatus.Expired;
                }
                else
                {
                    if (sms.VerifyCode == Code)
                        OutSMS.SMSVerifyStatus = SMSVerifyStatus.Success;
                    else
                        OutSMS.SMSVerifyStatus = SMSVerifyStatus.Failure;
                }
             //   OutSMS.OrderNo = sms.OrderNo;

                sms.SMSVerifyStatus = OutSMS.SMSVerifyStatus;
                _dbContext.SaveChanges();

            }

            return OutSMS;
        }
        public OutSMS RequireVerifyCode(string mobilePhone, int IntervalSec, SMSEvent SMSEvent = SMSEvent.UserPhoneVerify)
        {

            OutSMS OutSMS = new OutSMS();
            try
            {
                OutSMS = GetVerifyingSec(mobilePhone, IntervalSec);

                //说明可以重新发送短信（不在短信重新发送倒计时内）
                if (OutSMS.RemainSec == -1)
                {

                    string VerifyCode = GetRnd(6, true, false, false, false, "");

                    InSMS inSMS = new InSMS();
                    inSMS.Init();
                    inSMS.Tpl_id = Convert.ToInt32(SMSTemplate.NormalVerify).ToString();
                    inSMS.PhoneNumber = mobilePhone;
                    inSMS.Parameters = VerifyCode + "," + SMSMaxIntervalSec / 60;

                    if (!this.DoSMS(inSMS))
                    {
                        OutSMS.SMSVerifyStatus = SMSVerifyStatus.SentFailure;
                        return OutSMS;
                    }

                    ESMSVerification sms = new ESMSVerification()
                    {
                        VerifyCode = VerifyCode,
                        MobilePhone = mobilePhone,

                        SendDateTime = DateTime.Now,
                        SMSVerifyStatus = SMSVerifyStatus.Sent,
                        SMSEvent = SMSEvent,
                    };
                    _dbContext.DBSMSVerification.Add(sms);
                    _dbContext.SaveChanges();

                    OutSMS.SmsID = sms.ID;
                    OutSMS.SMSVerifyStatus = SMSVerifyStatus.Sent;
                    OutSMS.RemainSec = -1;
                }
                else
                {
                    OutSMS.RemainSec = IntervalSec - OutSMS.RemainSec;
                    OutSMS.SMSVerifyStatus = SMSVerifyStatus.Verifying;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OutSMS;
        }

        private OutSMS GetVerifyingSec(string mobilePhone, int IntervalSec, SMSEvent SMSEvent = SMSEvent.UserPhoneVerify)
        {
            OutSMS OutSMS = new OutSMS();
            try
            {
                var sms = _dbContext.DBSMSVerification.Where(s => s.MobilePhone == mobilePhone
                     && s.SMSEvent == SMSEvent
                     && s.SMSVerifyStatus == SMSVerifyStatus.Sent).OrderByDescending(s => s.SendDateTime).FirstOrDefault();

                OutSMS.RemainSec = -1;

                //存在
                if (sms != null)
                {
                    OutSMS.SmsID = sms.ID;
                  //  OutSMS.OrderNo = sms.OrderNo;

                    TimeSpan nowtimespan = new TimeSpan(DateTime.Now.Ticks);
                    TimeSpan endtimespan = new TimeSpan(sms.SendDateTime.Ticks);
                    TimeSpan timespan = nowtimespan.Subtract(endtimespan).Duration();
                    //是否超过倒计时最大值（比如90秒后重新发送，90秒为最大值）
                    int CurSec = Convert.ToInt32(timespan.TotalSeconds);
                    if (CurSec > IntervalSec)
                    {
                        return OutSMS;
                    }
                    else
                    {
                        OutSMS.RemainSec = CurSec;
                        return OutSMS;
                    }


                }
                return OutSMS;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Boolean DoSMS(InSMS inSMS)
        {
            Boolean result = true;
            ESMSLog smsLog = new ESMSLog();
            try
            {

                SMSCore sms = new SMSCore();

                SMSResult_API51 Response = sms.PostSMS_API51(inSMS, ref smsLog);
                if (Response.result == "0")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                smsLog.Exception += "DoSMS Error:" + ex.Message;
                smsLog.Exception += "DoSMS Inner Error:" + ex.InnerException.Message;
                result = false;
            }
            smsLog.IsSuccess = result;
            _dbContext.DBSMSLog.Add(smsLog);
            _dbContext.SaveChanges();
            

            return result;
        }
    }
}
