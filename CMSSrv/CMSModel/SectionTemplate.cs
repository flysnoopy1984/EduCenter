using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class SectionTemplate
    {
        public SectionTemplate()
        {
            SectionGroup = new HashSet<SectionGroup>();
        }

        public string TemplateName { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string ExampleData { get; set; }
        public int? Status { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<SectionGroup> SectionGroup { get; set; }
    }
}
