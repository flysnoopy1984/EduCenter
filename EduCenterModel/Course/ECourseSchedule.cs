using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Course
{
    /// <summary>
    /// 总课程表
    /// </summary>
    [Table("CourseSchedule")]
    public class ECourseSchedule: ECBaseModel
    {
        public ECourseSchedule()
        {
            Date = DateTime.MinValue;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 课程代号
        /// </summary>
        [MaxLength(20)]
        public string CourseCode { get; set; }

        [MaxLength(20)]
        public string CourseName { get; set; }

        [MaxLength(32)]
        public string TeaOpenId { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }
}
