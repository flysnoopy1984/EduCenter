using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ArticleListWidget
    {
        public string Id { get; set; }
        public int? ArticleTypeId { get; set; }
        public string DetailPageUrl { get; set; }
        public bool IsPageable { get; set; }
        public int? PageSize { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
