using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.BaseEnum
{
    public enum AmountTransType
    {
        /// <summary>
        /// 邀请用户，试听课后获取奖励
        /// </summary>
       Invited_TrialReward = 101,

       /// <summary>
       /// 邀请用户，正式报名支付
       /// </summary>
       Invited_Paied  = 102,

       /// <summary>
       /// 用户提现
       /// </summary>
       TransferToUser = 200,

    }
}
