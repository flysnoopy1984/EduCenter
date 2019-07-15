using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.QR
{
    [Table("QRInvite")]
    public class EQRInvite
    {
        public EQRInvite()
        {
          
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

       
        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public InviteQRType InviteQRType { get; set; }

        [MaxLength(256)]
        public string TargetUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(128)]
        public string FinalFilePath { get; set; }

        [MaxLength(128)]
        public string FileWithLogoPath { get; set; }

        [MaxLength(128)]
        public string OrigFilePath { get; set; }

        

        public RecordStatus RecordStatus { get; set; }

        public DateTime CreateDateTime { get; set; }




    }
}
