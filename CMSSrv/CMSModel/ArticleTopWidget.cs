using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ArticleTopWidget
    {
        public string Id { get; set; }
        public int? ArticleTypeId { get; set; }
        public int? Tops { get; set; }
        public string SubTitle { get; set; }
        public string MoreLink { get; set; }
        public string DetailPageUrl { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
