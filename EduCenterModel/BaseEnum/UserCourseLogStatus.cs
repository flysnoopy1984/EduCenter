using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.BaseEnum
{
    /// <summary>
    /// 大于等于10都是收费的
    /// </summary>
    public enum UserCourseLogStatus
    {
        
        /// <summary>
        ///  签到后，新建记录的状态
        /// </summary>
        PreNext =1,

        /// <summary>
        /// 请假
        /// </summary>
        Leave = 2,

        /// <summary>
        /// 老师请假
        /// </summary>
        TecLeave = 3,

    

        /// <summary>
        /// 
        /// </summary>
        SignIn = 10,
        /// <summary>
        ///  缺席，如果老师不做关闭课的动作，系统每天跑
        /// </summary>
        Absent = 11,

       

    }
}
