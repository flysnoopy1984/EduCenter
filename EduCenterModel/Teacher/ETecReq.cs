using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Teacher
{
    public class ETecReq
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public string WXName { get; set; }

        public string RealName { get; set; }

        public string Phone { get; set; }

        
    }
}
