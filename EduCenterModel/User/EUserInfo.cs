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
    public class EUserInfo: ECMasterDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(32)]
        public string OpenId { get; set; }

        [MaxLength(10)]
        public string RealName { get; set; }

        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        public UserRole UserRole { get; set; }

        public MemberType MemberType { get; set; }

        public int Sex { get; set; }

        /*冗余字段*/
        [MaxLength(10)]
        public string ChildName { get; set; }

        [MaxLength]
        public string wx_Name { get; set; }

        [MaxLength(20)]
        public string wx_city { get; set; }
        [MaxLength(20)]
        public string wx_province { get; set; }
        [MaxLength(20)]
        public string wx_country { get; set; }

        [MaxLength(256)]
        public string wx_headimgurl { get; set; }


    }
}
