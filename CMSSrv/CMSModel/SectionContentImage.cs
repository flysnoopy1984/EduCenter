using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class SectionContentImage
    {
        public string Id { get; set; }
        public string SectionWidgetId { get; set; }
        public string ImageSrc { get; set; }
        public string ImageAlt { get; set; }
        public string ImageTitle { get; set; }
        public string Href { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
