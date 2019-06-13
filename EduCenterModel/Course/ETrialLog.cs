using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Course
{
    [Table("TrialLog")]
    public class ETrialLog
    {
        public ETrialLog()
        {
            
            TrialDateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(20)]
        public string TecCode { get; set; }

        [MaxLength(20)]
        public string TecName { get; set; }

        [MaxLength(20)]
        public string CourseCode { get; set; }

        [MaxLength(50)]
        public string CourseName { get; set; }

        [MaxLength(32)]
        public string OpenId { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        public int Lesson { get; set; }
        public DateTime TrialDateTime { get; set; }

        public DateTime ApplyDateTime { get; set; }

        public TrialLogStatus TrialLogStatus { get; set; }

        [MaxLength(400)]
        /// <summary>
        /// 用户评论
        /// </summary>
        public string UserComment { get; set; }

        /// <summary>
        /// 用户打分
        /// </summary>
        public int UserRank { get; set; } 
       
    }
}
