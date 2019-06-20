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
    public class ECourseSchedule: ECMasterDataModel
    {
        public ECourseSchedule()
        {


        }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long Id { get; set; }

        [MaxLength(20)]
        public string CourseCode { get; set; }

        public CourseType CourseType { get; set; }

        [MaxLength(20)]
        public string CourseName { get; set; }


        /// <summary>
        ///  lessonCode = year + "_" + day + "_" + lesson + "_" + cCode + "_" + lessonNo+"_"+CourseScheduleType;
        /// </summary>
        [Key]
        [MaxLength(50)]
        public string LessonCode { get; set; }


        /// <summary>
        /// 同一个课在同一个时间段出现，区分1班2班
        /// </summary>
        public int LessonNo { get; set; }

        public int Year { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public int ApplyNum { get; set; }

      

        public CourseScheduleType CourseScheduleType { get; set; }

        // public CourseScheduleStatus Status { get; set; }

        //public string StartTime { get; set; }

        //public string EndTime { get; set; }

    }
}
