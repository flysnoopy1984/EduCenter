using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.BaseEnum
{
    public enum OrderStatus
    {
        Created = 0,
        WaitingPay =1,
        WaitingResponse = 2,
        PaySuccess =10,

        PayFailure = -1,
        Cancel = -2,

    }
}
