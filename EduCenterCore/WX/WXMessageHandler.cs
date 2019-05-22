using EduCenterModel.WX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EduCenterCore.WX
{
    public class WXMessageHandler
    {
        private WXMessage _WXMessage;
       // private MemoryStream _MemoryStream;
        public WXMessageHandler(MemoryStream ms)
        {
         
            string strXml = System.Text.Encoding.Default.GetString(ms.ToArray());
            if (!string.IsNullOrEmpty(strXml))
            {
                _WXMessage = new WXMessage();
                _WXMessage.LoadXml(strXml);


            }
        }

       
    }
}
