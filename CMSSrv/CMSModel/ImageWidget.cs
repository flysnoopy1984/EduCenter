using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ImageWidget
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string AltText { get; set; }
        public string Link { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
