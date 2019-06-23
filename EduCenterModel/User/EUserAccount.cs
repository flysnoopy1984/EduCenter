using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{

    /// <summary>
    /// 所有角色余额表
    /// </summary>
    [Table("UserAccount")]
    public class EUserAccount: ECBaseModel
    {
      
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }

        [Key]
        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public double RemainCourseTime { get; set; }

        public DateTime DeadLine { get; set; }

        //public double TotalCourseTime
        //{
        //    get; set;
        //}

        public double RemainSummerTime { get; set; }

        public DateTime SummerDeadLine { get; set; }

        public double RemainWinterTime { get; set; }

        public DateTime WinterDeadLine { get; set; }



    }
}
