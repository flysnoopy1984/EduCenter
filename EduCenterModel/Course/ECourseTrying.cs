using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Course
{
    [Table("CourseTrying")]
    public class ECourseTrying:ECBaseModel
    {
        public ECourseTrying()
        {
            Date = DateTime.MinValue;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long Id { get; set; }
        public long CourseId { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public long UserId { get; set; }

        public long ResponseTeaId { get; set; }

        /// <summary>
        /// 可以是用户的评价
        /// </summary>
        public string Remark { get; set; }

        

        /// <summary>
        /// 用户对于试听课的评价
        /// </summary>
        public int Score { get; set; }

      

        public CourseTryingStatus Status { get; set; }


    }
}
