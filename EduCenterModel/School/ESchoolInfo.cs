using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.School
{
    [Table("SchoolInfo")]
    public class ESchoolInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Key]
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(40)]
        public string Name { get; set; }
    }
}
