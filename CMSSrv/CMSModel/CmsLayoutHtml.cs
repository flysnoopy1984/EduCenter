using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class CmsLayoutHtml
    {
        public int LayoutHtmlId { get; set; }
        public string LayoutId { get; set; }
        public string PageId { get; set; }
        public string Html { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual CmsLayout Layout { get; set; }
    }
}
