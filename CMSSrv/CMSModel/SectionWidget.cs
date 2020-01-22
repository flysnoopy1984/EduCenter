using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class SectionWidget
    {
        public SectionWidget()
        {
            SectionContent = new HashSet<SectionContent>();
            SectionGroup = new HashSet<SectionGroup>();
        }

        public string Id { get; set; }
        public string SectionTitle { get; set; }
        public bool? IsHorizontal { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
        public virtual ICollection<SectionContent> SectionContent { get; set; }
        public virtual ICollection<SectionGroup> SectionGroup { get; set; }
    }
}
