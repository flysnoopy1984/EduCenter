using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Teacher
{
    [Table("TecSkill")]
    public class ETecSkill : ECBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(20)]
        public string TecCode { get; set; }

        public CourseType CourseType { get; set; }

        /// <summary>
        /// 熟练程度
        /// </summary>
        public SkillLevel SkillLevel { get; set; }


    }
}
