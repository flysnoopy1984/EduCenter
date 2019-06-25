using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
    /// <summary>
    /// 用户孩子
    /// </summary>
    [Table("UserChild")]
    public class EUserChild:ECBaseModel
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int No { get; set; }
        
        [MaxLength(32)]
        public string UserOpenId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public int Sex { get; set; }

        public int Age { get; set; }

        [MaxLength(10)]
        public string BirthDay { get; set; }

      
    }
}
