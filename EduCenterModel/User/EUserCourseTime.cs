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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public double RemainQty { get; set; }


        public CoursePriceType CoursePriceType { get; set; }

    }
}
