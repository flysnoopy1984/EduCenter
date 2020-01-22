using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ProductCategoryWidget
    {
        public string Id { get; set; }
        public int? ProductCategoryId { get; set; }
        public string TargetPage { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
