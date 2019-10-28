using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EduCenterModel.WX
{
    public class WXUserInfo
    {

        public int subscribe { get; set; }

        [MaxLength(32)]
        public string openid { get; set; }

        [MaxLength(40)]
        public string nickname { get; set; }
        public int sex { get; set; }
        [MaxLength(20)]
        public string language { get; set; }
        [MaxLength(20)]
        public string city { get; set; }
        [MaxLength(20)]
        public string province { get; set; }
        [MaxLength(20)]
        public string country { get; set; }

        [MaxLength(256)]
        public string headimgurl { get; set; }

        [MaxLength(32)]
        public string unionid { get; set; }


    }
}
