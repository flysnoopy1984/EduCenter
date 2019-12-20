using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.AppEdu
{
    [Table("appContentInfo")]
    public class EAppContentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Summary { get; set; }

        [MaxLength(255)]
        public string CoverUrl { get; set; }

        /*关联外部表*/
        public long NavId { get; set; }

        public long AuthorOpenId { get; set; }

        public long DetailId { get; set; }


    }
}
