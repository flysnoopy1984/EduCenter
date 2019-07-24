using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Sales
{
    [Table("InviteLog")]
    public class EInviteLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(32)]
        public string OwnOpenId { get; set; }

        [MaxLength(40)]
        public string OwnName { get; set; }

        [MaxLength(32)]
        public string InvitedOpenId { get; set; }

        public DateTime InvitedDateTime { get; set; }

        public InviteStatus InviteStatus { get; set; }


    }
}
