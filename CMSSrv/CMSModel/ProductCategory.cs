using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string Url { get; set; }
        public int? Status { get; set; }
        public string Seotitle { get; set; }
        public string SeokeyWord { get; set; }
        public string Seodescription { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
