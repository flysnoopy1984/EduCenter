using EduCenterModel.BaseEnum;
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
         
          
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        public string CourseCode { get; set; }

        public CourseType CourseType { get; set; }

        [MaxLength(20)]
        public string CourseName { get; set; }

        [MaxLength(20)]
        public string TecCode { get; set; }

        public int Year { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

      

        public CourseScheduleType CourseScheduleType { get; set; }

        // public CourseScheduleStatus Status { get; set; }

        //public string StartTime { get; set; }

        //public string EndTime { get; set; }

    }
}
