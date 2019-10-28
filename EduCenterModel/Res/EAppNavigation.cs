using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Res
{
    [Table("appNavigation")]
    public class EAppNavigation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(20)]
        public string ShowName { get; set; }

        public int Level { get; set; }

        public AppNavModule Module { get; set; }

        [MaxLength(255)]
        public string IconFilePath { get; set; }

        [MaxLength(50)]
        public string TargetAppPageName { get; set; }

        public int Position { get; set; }
        public RecordStatus RecordStatus { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
