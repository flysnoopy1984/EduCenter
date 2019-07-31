using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.BaseEnum
{
    public enum UserRole
    {
        BlackList = -1,
        Visitor = 0,
        /// <summary>
        /// 有上过正式课程的用户
        /// </summary>
        Member =1,

        Teacher = 10,

        Sales = 20,

        Assist = 30,

        //Manager =50,

        Admin = 100,

    }
}
