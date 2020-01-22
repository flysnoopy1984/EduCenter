using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class HtmlWidget
    {
        public string Id { get; set; }
        public string Html { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
