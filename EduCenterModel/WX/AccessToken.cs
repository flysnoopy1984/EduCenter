using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }

        public string errcode { get; set; }

        public string errmsg { get; set; }

        public string openid { get; set; }

        public string unionid { get; set; }

        public DateTime expire_DateTime { get; set; }
    }
}
