using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class SectionContent
    {
        public string Id { get; set; }
        public string SectionWidgetId { get; set; }
        public string SectionGroupId { get; set; }
        public int? SectionContentType { get; set; }
        public int? Order { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual SectionWidget SectionWidget { get; set; }
    }
}
