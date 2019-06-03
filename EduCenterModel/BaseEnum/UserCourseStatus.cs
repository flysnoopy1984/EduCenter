using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.BaseEnum
{
    public enum UserCourseStatus
    {

        Avaliable = 0,
        /// <summary>
        /// 等待支付
        /// </summary>
        WaitingPay = -1,
        /// <summary>
        /// 过了有效期
        /// </summary>
        OutofData = -2,


    }
}
