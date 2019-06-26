using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{

    /// <summary>
    /// 用户课程表
    /// </summary>
    [Table("UserCourse")]
    public class EUserCourse : ECBaseModel
    {
        public EUserCourse()
        {
            CreateDateTime = DateTime.Now;
         
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }


        [MaxLength(50)]
        public string LessonCode { get; set; }

        public CourseScheduleType CourseScheduleType { get; set; }

        //public UserCourseStatus UserCourseStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

       



    }
}
