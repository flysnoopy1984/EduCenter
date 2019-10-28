using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX
{
    public class MiniCode2Session
    {
        public string openId { get; set; }

        public string session_key { get; set; }

        public string unionid { get; set; }

        public bool HasExistInWX { get; set; }

        public int errcode { get; set; }

        public string errmsg { get; set; }
    }
}
