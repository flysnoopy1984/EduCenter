using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class CarouselWidget
    {
        public CarouselWidget()
        {
            CarouselItem = new HashSet<CarouselItem>();
        }

        public string Id { get; set; }
        public int? CarouselId { get; set; }

        public virtual Carousel Carousel { get; set; }
        public virtual CmsWidgetBase IdNavigation { get; set; }
        public virtual ICollection<CarouselItem> CarouselItem { get; set; }
    }
}
