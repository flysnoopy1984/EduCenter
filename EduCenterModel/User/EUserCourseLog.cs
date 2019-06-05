using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
    [Table("UserCourseLog")]
    public class EUserCourseLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(50)]
        public string LessonCode { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public UserCourseLogStatus UserCourseLogStatus { get; set; }

        public CourseScheduleType CourseScheduleType { get; set; }

        public DateTime CreatedDateTime { get; set; }  
    }
}
