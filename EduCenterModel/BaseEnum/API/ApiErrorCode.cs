using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.BaseEnum.API
{
    public enum ApiErrorCode
    {
        UserLogin_NoPhone = 1001,
        UserLogin_NoPwd = 1005,
        UserLogin_WrongPwd = 1002,
        UserLogin_NoUser = 1003,

        UserLogin_NoToken = 1004
    }
}
