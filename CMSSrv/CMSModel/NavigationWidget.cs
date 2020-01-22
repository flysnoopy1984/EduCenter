using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class NavigationWidget
    {
        public string Id { get; set; }
        public string CustomerClass { get; set; }
        public string Title { get; set; }
        public string Logo { get; set; }
        public string AlignClass { get; set; }
        public bool? IsTopFix { get; set; }
        public string RootId { get; set; }
        public bool? ShowBasket { get; set; }

        public virtual CmsWidgetBase IdNavigation { get; set; }
    }
}
