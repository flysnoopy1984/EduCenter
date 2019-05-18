﻿using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Teacher
{
    [Table("TeaSkill")]
    public class ETeaSkill : ECBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string TecCode { get; set; }

        public string CourseCode { get; set; }

        /// <summary>
        /// 熟练程度
        /// </summary>
        public SkillLevel SkillLevel { get; set; }


    }
}
