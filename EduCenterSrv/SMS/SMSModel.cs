using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterSrv.SMS
{
    public class SMSResult_API51
    {
        public string result { get; set; }
        public string errmsg { get; set; }
        public string ext { get; set; }

        public string sid { get; set; }

        public string fee { get; set; }
    }

    [Table("SMSLog")]
    public class ESMSLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [MaxLength(30)]
        public string APPName { get; set; }

        [MaxLength(15)]
        public string UserPhone { get; set; }

        public DateTime SendDateTime { get; set; }

        [MaxLength(100)]
        public string RequestMessage { get; set; }

        [MaxLength(200)]
        public string ResponseMessage { get; set; }

        public bool IsSuccess { get; set; }

        [MaxLength(200)]
        public string Exception { get; set; }


    }

    [Table("SMSVerification")]
    public class ESMSVerification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [MaxLength(64)]
        public string OrderNo { get; set; }

        [MaxLength(10)]
        public string VerifyCode { get; set; }

        public SMSEvent SMSEvent { get; set; }

        [MaxLength(20)]
        public string MobilePhone { get; set; }

        public SMSVerifyStatus SMSVerifyStatus { get; set; }

        private DateTime _SendDateTime = DateTime.MaxValue;

        public DateTime SendDateTime
        {
            get
            {
                return _SendDateTime;
            }
            set
            {
                _SendDateTime = value;
            }
        }


    }

    public class InSMS
    {
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 模板编号
        /// </summary>
        public string Tpl_id { get; set; }

        public void Init()
        {

            this.Sign = "玉杰投资服务";
            //   this.Tpl_id = "49551"; //您的验证码为:{1}.您的订单编号为:{2}.请在支付后到以下地址进行收款确认:{3}。
            //  this.Tpl_id = "50959";//您的验证码为:{1}.您的收款确认码为:{2}.，请在支付后到以下地址用收款确认码进行收款确认:{3}。

        }
    }

    public class OutSMS
    {
        public long SmsID { get; set; }
        public SMSVerifyStatus SMSVerifyStatus { get; set; }

        public int RemainSec { get; set; }

    }
}
