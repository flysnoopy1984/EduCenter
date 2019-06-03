using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Course
{
    [Table("CourseInfoClass")]
    public class ECourseInfoClass: ECMasterDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string ClassName { get; set; }

        public string CourseCode { get; set; }

        public string TecCode { get; set; }
    }
}
