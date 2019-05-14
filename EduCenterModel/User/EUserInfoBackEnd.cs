using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
    [Table("UserInfoBackEnd")]
    public class EUserInfoBackEnd:ECBaseModel
    {
        public EUserInfoBackEnd()
        {
            LastLoginDateTime = DateTime.MinValue;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        public string LoginName { get; set; }

        [MaxLength(30)]
        public string LoginPwd { get; set; }

        public DateTime LastLoginDateTime { get; set; }

        [MaxLength(32)]
        public string UserOpenId { get; set; }
    }
}
