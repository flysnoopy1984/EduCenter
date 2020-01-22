using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string PagePath { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public string UserName { get; set; }
        public string CommentContent { get; set; }
        public int? Agrees { get; set; }
        public int? Disagrees { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
