using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Course
{
    [Table("CoursePrice")]
    public class ECoursePrice: ECMasterDataModel
    {
        [Key]
        [MaxLength(20)]
        public string PriceCode { get; set; }

        [MaxLength(50)]
        public string PriceName { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        public double Price { get; set; }

        public CoursePriceType CoursePriceType { get; set; }

        public DateTime EffectStartDate { get; set; }

        public DateTime EffectEndDate { get; set; }
    }
}
