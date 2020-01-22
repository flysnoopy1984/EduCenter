using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ProductDetailWidget
    {
        public string Id { get; set; }
        public string CustomerClass { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
