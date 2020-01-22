using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class UserRoleRelation
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string UserId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual Users User { get; set; }
    }
}
