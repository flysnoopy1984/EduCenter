using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class VideoWidget
    {
        public string Id { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
