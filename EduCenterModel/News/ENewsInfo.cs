using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.News
{
    [Table("miniNewsInfo")]
    public class ENewsInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

      
        [MaxLength(100)]
        public string wxMediaID { get; set; }

        public NewsSource NewsSource { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string PageUrl { get; set; }

       
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        [MaxLength(255)]
        public string CoverImgUrl { get; set; }

        [MaxLength(50)]
        public string Auther { get; set; }

        public NewsPublishStatus PublishStatus { get; set; }

    }
}
