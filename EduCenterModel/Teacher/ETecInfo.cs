using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Teacher
{
    public class ETecInfo: ECBaseModel
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public string Name { get; set; }

        public int Sex { get; set; }

        public DateTime JoinDateTime { get; set; }


    }
}
