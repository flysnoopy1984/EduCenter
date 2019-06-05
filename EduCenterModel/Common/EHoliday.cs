using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Common
{
    [Table("Holiday")]
    public class EHoliday
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public DateTime HolidayDate { get; set; }
    }
}
