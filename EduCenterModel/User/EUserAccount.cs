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
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [MaxLength(32)]
        public string UserOpenId { get; set; }

        public double RemainCourseTime { get; set; }

        public DateTime DeadLine { get; set; }

     
        public double RemainSummerTime { get; set; }

        public DateTime SummerDeadLine { get; set; }

        public double RemainWinterTime { get; set; }

        public DateTime WinterDeadLine { get; set; }

        public bool CanSelectCourse { get; set; }

        public bool CanSelectSummerWinterCourse { get; set; }

        public DateTime BuyDate { get; set; }

        public DateTime SummerBuyDate { get; set; }

        public DateTime WinterBuyDate { get; set; }

        /// <summary>
        /// Vip一人一单价
        /// </summary>
        public double VIPPrice1 { get; set; }

        /// <summary>
        ///邀请奖励
        /// </summary>
        public double InviteRewards { get; set; }

        /// <summary>
        /// 剩余奖励
        /// </summary>
        public double RemainRewards { get; set; }

        /// <summary>
        /// 特殊化处理，每次课时扣减，0为标准，大于0按数值计算
        /// </summary>
        public int ReduceTime { get; set; }

        /// <summary>
        /// 支付宝账户
        /// </summary>
        [MaxLength(100)]
        public string AliPayAccount { get; set; }


    }
}
