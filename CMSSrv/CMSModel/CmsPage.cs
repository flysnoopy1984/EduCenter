using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class CmsPage
    {
        public string Id { get; set; }
        public string ReferencePageId { get; set; }
        public bool? IsPublishedPage { get; set; }
        public string ParentId { get; set; }
        public string PageName { get; set; }
        public bool IsHomePage { get; set; }
        public string Url { get; set; }
        public string LayoutId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public bool IsPublish { get; set; }
        public DateTime? PublishDate { get; set; }
        public string MetaKeyWorlds { get; set; }
        public string MetaDescription { get; set; }
        public string Script { get; set; }
        public string Style { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual CmsLayout Layout { get; set; }
    }
}
