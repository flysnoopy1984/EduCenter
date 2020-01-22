using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ArticleTypeWidget
    {
        public string Id { get; set; }
        public int? ArticleTypeId { get; set; }
        public string TargetPage { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
