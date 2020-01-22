using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class Navigation
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public bool? IsMobile { get; set; }
        public string Html { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int? DisplayOrder { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
