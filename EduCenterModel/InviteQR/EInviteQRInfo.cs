using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.InviteQR
{
    [Table("InviteQRInfo")]
    public class EInviteQRInfo
    {
        [Key]
        [MaxLength(20)]
        public string Id { get; set; }

        public string UserOpenId { get; set; }

        public InviteQRType InviteQRType { get; set; }

        [MaxLength(256)]
        public string TargetUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(128)]
        public string FilePath { get; set; }

        public RecordStatus RecordStatus { get; set; }




    }
}
