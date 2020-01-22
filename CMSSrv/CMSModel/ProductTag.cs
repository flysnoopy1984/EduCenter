using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ProductTag
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? TagId { get; set; }
        public string Title { get; set; }
    }
}
