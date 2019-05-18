﻿using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Teacher
{
    [Table("TecInfo")]
    public class ETecInfo: ECMasterDataModel
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Key]
        [MaxLength(20)]
        public string TecCode { get; set; }

       
        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public string Name { get; set; }

        public int Sex { get; set; }

  

       


    }
}