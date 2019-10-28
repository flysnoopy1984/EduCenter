using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Res
{
    [Table("appBanner")]
    public class EAppBanner 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(255)]
        public string BannerImg { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }


        public int Position { get; set; }

        public RecordStatus RecordStatus { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
