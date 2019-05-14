using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
    /// <summary>
    /// 用户基础信息表
    /// </summary>
    [Table("UserInfo")]
    public class EUserInfo: ECBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(32)]
        public string OpenId { get; set; }

        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        public UserRole UserRole { get; set; }

        /*冗余字段*/
        public string ChildName { get; set; }
    }
}
