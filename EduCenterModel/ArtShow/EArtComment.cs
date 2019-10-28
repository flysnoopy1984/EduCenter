using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.ArtShow
{
    [Table("miniArtComment")]
    public class EArtComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long RefId { get; set; }

        public long ArtId { get; set; }

        [MaxLength(32)]
        public string UnionId { get; set; }

        [MaxLength(255)]
        public string Content { get; set; }

        public int Praize { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
