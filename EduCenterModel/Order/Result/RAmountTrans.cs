using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Order.Result
{
    public class RAmountTrans
    {
        public string UserOpenId { get; set; }

        public string TransTypeName { get; set; }
        public string TransDate { get; set; }
        public string Amount { get; set; }

        public AmountTransDirection transDirection { get; set; }
    }
}
