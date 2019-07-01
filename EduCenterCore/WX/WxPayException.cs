using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterCore.WX
{
    public class WxPayException : Exception
    {
        public WxPayException(string msg) : base(msg)
        {

        }
    }
}
