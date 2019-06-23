using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserCharge
    {
        public string UserOpenId { get; set; }

        public string ItemName { get; set; }

        //缴费时间
        public string CreateDateTime { get; set; }

        //缴费金额
        public string Amount { get; set; }

        //项目数量
        public string Qty { get; set; }
    }
}
