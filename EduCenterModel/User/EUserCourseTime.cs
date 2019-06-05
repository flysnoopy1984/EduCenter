using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
   
    [Table("UserCourseTime")]
    public class EUserCourseTime : ECBaseModel
    {
        public EUserCourseTime()
        {
            ReNewDateTime = DateTime.MinValue;
            if (ReNewDateTime == DateTime.MinValue)
                InValidDateTime = CreateDateTime.AddYears(1);
            else
                InValidDateTime = ReNewDateTime.AddYears(1);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public double RemainQty { get; set; }


        public CourseScheduleType CourseScheduleType { get; set; }

        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 续费时间
        /// </summary>
        public DateTime ReNewDateTime { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime InValidDateTime { get; set; }

    }
}
