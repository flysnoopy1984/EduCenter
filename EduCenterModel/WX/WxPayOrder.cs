using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX
{
    public class WxPayOrder
    {
        public WxPayOrder()
        {
            IsSuccess = true;
        }
        public string appId { get; set; }
        public string timeStamp { get; set; }
        public string nonceStr { get; set; }
        public string package { get; set; }

        public string signType { get; set; }
        public string paySign { get; set; }

        public string OrderNo { get; set; }

        public string EduOrderNo { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMsg { get; set; }

        public int IntMsg { get; set; }


    }
}
