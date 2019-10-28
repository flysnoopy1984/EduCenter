using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.ArtShow
{

    [Table("miniArtInfo")]
    public class EArtInfo: ECBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public CourseType CourseType { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Desc { get; set; }

        [MaxLength(120)]
        public string CoverFilePath { get; set; }

        [MaxLength(32)]
        public string UnionId { get; set; }

        [MaxLength(40)]
        public string UploadUser { get; set; }

        public int Praize { get; set; }

        public int Comments{ get; set; }

        public ArtMediaType ArtMediaType { get; set; }


        public DateTime UploadDateTime { get; set; }

        public RecordStatus RecordStatus { get; set; }
    }
}
