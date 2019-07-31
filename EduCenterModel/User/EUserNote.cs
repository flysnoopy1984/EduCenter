using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
    /// <summary>
    /// 接待人员备注
    /// </summary>
    [Table("UserNote")]
    public class EUserNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(255)]
        public string Content { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public DateTime CreateDateTime { get; set; }

        [MaxLength(40)]
        public string CreateBy { get; set; }

        //public DateTime UpdateDateTime { get; set; }
        //[MaxLength(20)]
        //public string UpdateBy { get; set; }

    }
}
