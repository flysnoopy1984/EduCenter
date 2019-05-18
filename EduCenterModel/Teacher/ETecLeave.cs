using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Teacher
{
    [Table("TecLeave")]
    public class ETecLeave: ECBaseModel
    {
        public ETecLeave()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string TecCode { get; set; }

        public LeaveStatus LeaveStatus { get; set; }

        public long CourseScheduleId { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; } 
    }
}
