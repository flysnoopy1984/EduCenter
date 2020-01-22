using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ProductListWidget
    {
        public string Id { get; set; }
        public bool IsPageable { get; set; }
        public int? ProductCategoryId { get; set; }
        public string DetailPageUrl { get; set; }
        public string Columns { get; set; }
        public int? PageSize { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
