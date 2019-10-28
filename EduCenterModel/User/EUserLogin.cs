using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
    [Table("UserLogin")]
    public class EUserLogin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public AppSystem AppSystem { get; set; }

        [MaxLength(20)]
        public string LoginKey { get; set; }

        [MaxLength(20)]
        public string Pwd { get; set; }


        [MaxLength(32)]
        public string WxOpenId { get; set; }

        /// <summary>
        /// 当前Token
        /// </summary>
        [MaxLength(32)]
        public string Token { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime EffectDate { get; set; }

    }
}
