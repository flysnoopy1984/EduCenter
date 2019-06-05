using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.BaseEnum
{
    public enum UserCourseLogStatus
    {
        
        /// <summary>
        ///  签到后，新建记录的状态
        /// </summary>
        PreNext =1,
        /// <summary>
        /// 老师开课后
        /// </summary>
        Started =2,
        /// <summary>
        /// 用户签到后
        /// </summary>
        SignIn = 10,
        /// <summary>
        ///  缺席，如果老师不做关闭课的动作，系统每天跑
        /// </summary>
        Absent = -1,

    }
}
