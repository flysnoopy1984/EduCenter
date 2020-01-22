using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class Users
    {
        public Users()
        {
            UserRoleRelation = new HashSet<UserRoleRelation>();
        }

        public string UserId { get; set; }
        public string PassWord { get; set; }
        public string ApiLoginToken { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LoginIp { get; set; }
        public string PhotoUrl { get; set; }
        public long? Timestamp { get; set; }
        public string UserName { get; set; }
        public int? UserTypeCd { get; set; }
        public string Address { get; set; }
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }
        public string Birthplace { get; set; }
        public string Email { get; set; }
        public string EnglishName { get; set; }
        public string FirstName { get; set; }
        public string Hobby { get; set; }
        public string LastName { get; set; }
        public int? MaritalStatus { get; set; }
        public string MobilePhone { get; set; }
        public string NickName { get; set; }
        public string Profession { get; set; }
        public string Qq { get; set; }
        public string School { get; set; }
        public int? Sex { get; set; }
        public string Telephone { get; set; }
        public string ZipCode { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? Status { get; set; }
        public string Description { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenDate { get; set; }

        public virtual ICollection<UserRoleRelation> UserRoleRelation { get; set; }
    }
}
