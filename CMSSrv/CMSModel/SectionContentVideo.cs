using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class SectionContentVideo
    {
        public string Id { get; set; }
        public string VideoTitle { get; set; }
        public string Thumbnail { get; set; }
        public string SectionWidgetId { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
    }
}
