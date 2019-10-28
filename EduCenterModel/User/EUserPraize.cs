using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
    [Table("miniUserPraize")]
    public class EUserPraize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [MaxLength(32)]
        public string UnionId { get; set; }

        public long RefId { get; set; }

        public PraizeTarget PraizeTarget { get; set; }

        public DateTime PraizeDateTime { get; set; }
    }
}
