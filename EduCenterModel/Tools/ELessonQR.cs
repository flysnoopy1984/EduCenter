using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Tools
{
    [Table("tool_LessonQR")]
    public class ELessonQR
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Url { get; set; }

        [MaxLength(128)]
        public string QRFilePath { get; set; }

        public DateTime CreateDateTime { get; set; }

        public RecordStatus RecordStatus { get; set; }

    }
}
