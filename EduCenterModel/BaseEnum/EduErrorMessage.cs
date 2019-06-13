using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.BaseEnum
{
    public enum EduErrorMessage
    {
        NoError = 0,
        ApplyTrial_OverAllLimit = 200, //超过试听课总数6次
        ApplyTrial_OverSingleLimit = 201,//超过单节试听课2次
        ApplyTrial_Exist = 202,


    }
}
