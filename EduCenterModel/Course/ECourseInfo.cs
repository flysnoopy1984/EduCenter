using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Course
{
    [Table("CourseInfo")]
    public class ECourseInfo: ECBaseModel
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 课程代号
        /// </summary>
        [Key]
        [MaxLength(20)]
        public string Code { get; set; }
        /// <summary>
        /// 课程名
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; }

        public CourseType CourseType { get; set; }

        /// <summary>
        /// 课时长
        /// </summary>
        public double CourseHours { get; set; }
    }
}
