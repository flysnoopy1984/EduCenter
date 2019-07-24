using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Sales
{
    [Table("InviteRewardTrans")]
    public class EInviteRewardTrans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long InviteLogId { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public AmountTransDirection Direction { get; set; }

        public double Amount { get; set; }

        public DateTime TransDateTime { get; set; }

        public AmountTransType TransType { get; set; }

        public AmountTransStatus TransStatus { get; set; }


    }
}
