using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
    [Table("UserCourseTimeTrans")]
    public class EUserCourseTimeTrans: ECBaseModel
    {
        public EUserCourseTimeTrans()
        {
            TransDateTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }


        public double TransQty { get; set; }

        public CoursePriceType CoursePriceType { get; set; }

        [MaxLength(20)]
        public string CoursePriceCode { get; set; }

        public DateTime TransDateTime { get; set; }
    }
}
