using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.BaseEnum
{
    public enum TrialLogStatus
    {
        //0-10  代表这条记录其实属于空
        Cancel = 2,
        UserNotCome = 3,

        //10-100 代表在途且是一条有效记录
        UserApply = 10,
        TecConfirm = 11,

        //100+ 是一条有效记录
        Done = 100,
        


    }
}
