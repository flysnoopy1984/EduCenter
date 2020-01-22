using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class StyleSheetWidget
    {
        public string Id { get; set; }
        public string StyleSheet { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
