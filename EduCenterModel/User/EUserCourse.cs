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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }


        [MaxLength(50)]
        public string LessonCode { get; set; }

        public CoursePriceType CoursePriceType { get; set; }

    }
}
