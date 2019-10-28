using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.ArtShow
{
    [Table("miniArtDetail")]
    public class EArtDetail: ECBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ArtId { get; set; }

        [MaxLength(120)]
        public string FilePath { get; set; }
    }
}
