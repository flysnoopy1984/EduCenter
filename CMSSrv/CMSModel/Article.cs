using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public int? Counter { get; set; }
        public int? ArticleTypeId { get; set; }
        public string Description { get; set; }
        public string ArticleContent { get; set; }
        public int? Status { get; set; }
        public string ImageThumbUrl { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPublish { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Url { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual ArticleType ArticleType { get; set; }
    }
}
