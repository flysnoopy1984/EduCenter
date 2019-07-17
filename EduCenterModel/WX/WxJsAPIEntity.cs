using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX
{
    public class WxJsAPIEntity
    {
        public WxJsAPIEntity()
        {
            IsSuccess = true;
        }
        public bool debug { get; set; }
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }

        public string openId { get; set; }

        public bool IsSuccess { get; set; }

        public int IntMsg { get; set; }

        private string _ErrorMsg;
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set
            {
                IsSuccess = false;
                _ErrorMsg = value;
              
            }
        }
    }
}
