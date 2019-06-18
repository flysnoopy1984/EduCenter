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

        [MaxLength(20)]
        public string TecCode { get; set; }

        [MaxLength(20)]
        public string TecName { get; set; }

        //public LeaveStatus LeaveStatus { get; set; }

        public DateTime LeaveDate { get; set; }

        public DateTime ApplyDateTime { get; set; }

        public LeaveType LeaveType { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; } 
    }
}
