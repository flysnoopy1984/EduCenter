﻿using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class Roles
    {
        public Roles()
        {
            Permission = new HashSet<Permission>();
            UserRoleRelation = new HashSet<UserRoleRelation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<Permission> Permission { get; set; }
        public virtual ICollection<UserRoleRelation> UserRoleRelation { get; set; }
    }
}
