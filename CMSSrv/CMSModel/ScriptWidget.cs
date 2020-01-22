using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ScriptWidget
    {
        public string Id { get; set; }
        public string Script { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
