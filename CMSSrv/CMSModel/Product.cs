using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbUrl { get; set; }
        public int? BrandCd { get; set; }
        public int? ProductCategoryId { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }
        public decimal? RebatePrice { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string Norm { get; set; }
        public string ShelfLife { get; set; }
        public string ProductContent { get; set; }
        public string Description { get; set; }
        public bool IsPublish { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? Status { get; set; }
        public string Seotitle { get; set; }
        public string SeokeyWord { get; set; }
        public string Seodescription { get; set; }
        public int? OrderIndex { get; set; }
        public string SourceFrom { get; set; }
        public string Url { get; set; }
        public string TargetFrom { get; set; }
        public string TargetUrl { get; set; }
        public string PartNumber { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string Author { get; set; }
        public string Translator { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
