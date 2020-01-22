using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class CmsWidgetBase
    {
        public string Id { get; set; }
        public string WidgetName { get; set; }
        public string Title { get; set; }
        public int? Position { get; set; }
        public string LayoutId { get; set; }
        public string PageId { get; set; }
        public string ZoneId { get; set; }
        public string PartialView { get; set; }
        public string AssemblyName { get; set; }
        public string ServiceTypeName { get; set; }
        public string ViewModelTypeName { get; set; }
        public string FormView { get; set; }
        public string StyleClass { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public bool? IsTemplate { get; set; }
        public string Thumbnail { get; set; }
        public bool? IsSystem { get; set; }
        public string ExtendData { get; set; }
        public int? RuleId { get; set; }

        public virtual ArticleDetailWidget ArticleDetailWidget { get; set; }
        public virtual ArticleListWidget ArticleListWidget { get; set; }
        public virtual ArticleSummaryWidget ArticleSummaryWidget { get; set; }
        public virtual ArticleTopWidget ArticleTopWidget { get; set; }
        public virtual ArticleTypeWidget ArticleTypeWidget { get; set; }
        public virtual CarouselWidget CarouselWidget { get; set; }
        public virtual HtmlWidget HtmlWidget { get; set; }
        public virtual ImageWidget ImageWidget { get; set; }
        public virtual NavigationWidget NavigationWidget { get; set; }
        public virtual ProductCategoryWidget ProductCategoryWidget { get; set; }
        public virtual ProductDetailWidget ProductDetailWidget { get; set; }
        public virtual ProductListWidget ProductListWidget { get; set; }
        public virtual ScriptWidget ScriptWidget { get; set; }
        public virtual SectionWidget SectionWidget { get; set; }
        public virtual StyleSheetWidget StyleSheetWidget { get; set; }
        public virtual VideoWidget VideoWidget { get; set; }
    }
}
