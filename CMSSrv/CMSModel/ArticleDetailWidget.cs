using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ArticleDetailWidget
    {
        public string Id { get; set; }
        public string CustomerClass { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
