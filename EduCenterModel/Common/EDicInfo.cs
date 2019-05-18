using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Common
{
    [Table("DicInfo")]
    public class EDicInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string DicType { get; set; } 

        /// <summary>
        /// 0则没有父亲
        /// </summary>
        public long pId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        
    }
}
