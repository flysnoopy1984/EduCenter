using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ArticleSummaryWidget
    {
        public string Id { get; set; }
        public string SubTitle { get; set; }
        public string Style { get; set; }
        public string DetailLink { get; set; }
        public string Summary { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
