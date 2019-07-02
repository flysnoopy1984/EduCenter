using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX
{
    public class WxPayInfo
    {
        public string ItemDes { get; set; }

        public float PayAmount { get; set; }

        /// <summary>
        /// 买家的OpenId
        /// </summary>
        public string OpenId { get; set; }
    }
}
