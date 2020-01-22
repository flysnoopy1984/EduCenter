using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class CmsLayout
    {
        public CmsLayout()
        {
            CmsLayoutHtml = new HashSet<CmsLayoutHtml>();
            CmsPage = new HashSet<CmsPage>();
            CmsZone = new HashSet<CmsZone>();
        }

        public string Id { get; set; }
        public string LayoutName { get; set; }
        public string Title { get; set; }
        public string ContainerClass { get; set; }
        public int? Status { get; set; }
        public string Description { get; set; }
        public string Script { get; set; }
        public string Style { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbUrl { get; set; }
        public string Theme { get; set; }

        public virtual ICollection<CmsLayoutHtml> CmsLayoutHtml { get; set; }
        public virtual ICollection<CmsPage> CmsPage { get; set; }
        public virtual ICollection<CmsZone> CmsZone { get; set; }
    }
}
