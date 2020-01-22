using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CMSSrv.CMSModel;

namespace CMSSrv.DataBase
{
    public partial class CmsDbContext : DbContext
    {
        public CmsDbContext()
        {
        }

        public CmsDbContext(DbContextOptions<CmsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationSetting> ApplicationSetting { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleDetailWidget> ArticleDetailWidget { get; set; }
        public virtual DbSet<ArticleListWidget> ArticleListWidget { get; set; }
        public virtual DbSet<ArticleSummaryWidget> ArticleSummaryWidget { get; set; }
        public virtual DbSet<ArticleTopWidget> ArticleTopWidget { get; set; }
        public virtual DbSet<ArticleType> ArticleType { get; set; }
        public virtual DbSet<ArticleTypeWidget> ArticleTypeWidget { get; set; }
        public virtual DbSet<Basket> Basket { get; set; }
        public virtual DbSet<Carousel> Carousel { get; set; }
        public virtual DbSet<CarouselItem> CarouselItem { get; set; }
        public virtual DbSet<CarouselWidget> CarouselWidget { get; set; }
        public virtual DbSet<CmsLayout> CmsLayout { get; set; }
        public virtual DbSet<CmsLayoutHtml> CmsLayoutHtml { get; set; }
        public virtual DbSet<CmsMedia> CmsMedia { get; set; }
        public virtual DbSet<CmsMessage> CmsMessage { get; set; }
        public virtual DbSet<CmsPage> CmsPage { get; set; }
        public virtual DbSet<CmsRedirection> CmsRedirection { get; set; }
        public virtual DbSet<CmsRule> CmsRule { get; set; }
        public virtual DbSet<CmsTheme> CmsTheme { get; set; }
        public virtual DbSet<CmsWidgetBase> CmsWidgetBase { get; set; }
        public virtual DbSet<CmsZone> CmsZone { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<DataArchived> DataArchived { get; set; }
        public virtual DbSet<DataDictionary> DataDictionary { get; set; }
        public virtual DbSet<ExtendField> ExtendField { get; set; }
        public virtual DbSet<FormData> FormData { get; set; }
        public virtual DbSet<FormDataItem> FormDataItem { get; set; }
        public virtual DbSet<Forms> Forms { get; set; }
        public virtual DbSet<HtmlWidget> HtmlWidget { get; set; }
        public virtual DbSet<ImageWidget> ImageWidget { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<MainListPartWidget> MainListPartWidget { get; set; }
        public virtual DbSet<Navigation> Navigation { get; set; }
        public virtual DbSet<NavigationWidget> NavigationWidget { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<PageView> PageView { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductCategoryTag> ProductCategoryTag { get; set; }
        public virtual DbSet<ProductCategoryWidget> ProductCategoryWidget { get; set; }
        public virtual DbSet<ProductDetailWidget> ProductDetailWidget { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<ProductListWidget> ProductListWidget { get; set; }
        public virtual DbSet<ProductTag> ProductTag { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<ScriptWidget> ScriptWidget { get; set; }
        public virtual DbSet<SectionContent> SectionContent { get; set; }
        public virtual DbSet<SectionContentCallToAction> SectionContentCallToAction { get; set; }
        public virtual DbSet<SectionContentImage> SectionContentImage { get; set; }
        public virtual DbSet<SectionContentParagraph> SectionContentParagraph { get; set; }
        public virtual DbSet<SectionContentTitle> SectionContentTitle { get; set; }
        public virtual DbSet<SectionContentVideo> SectionContentVideo { get; set; }
        public virtual DbSet<SectionGroup> SectionGroup { get; set; }
        public virtual DbSet<SectionTemplate> SectionTemplate { get; set; }
        public virtual DbSet<SectionWidget> SectionWidget { get; set; }
        public virtual DbSet<StyleSheetWidget> StyleSheetWidget { get; set; }
        public virtual DbSet<UserRoleRelation> UserRoleRelation { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VideoWidget> VideoWidget { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=47.101.130.0\\\\\\\\IQBSQLSERVER,1433;User Id=sa;Password=qwer@1234;Database=EduCMS");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationSetting>(entity =>
            {
                entity.HasKey(e => e.SettingKey);

                entity.Property(e => e.SettingKey).HasMaxLength(50);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArticleTypeId).HasColumnName("ArticleTypeID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImageThumbUrl).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.MetaDescription).HasMaxLength(255);

                entity.Property(e => e.MetaKeyWords).HasMaxLength(255);

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.Summary).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(100);

                entity.HasOne(d => d.ArticleType)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.ArticleTypeId)
                    .HasConstraintName("FK_Article_ArticleCategory");
            });

            modelBuilder.Entity<ArticleDetailWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerClass).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ArticleDetailWidget)
                    .HasForeignKey<ArticleDetailWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleDetailWidget_Widget");
            });

            modelBuilder.Entity<ArticleListWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.ArticleTypeId).HasColumnName("ArticleTypeID");

                entity.Property(e => e.DetailPageUrl).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ArticleListWidget)
                    .HasForeignKey<ArticleListWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleListWidget_Widget");
            });

            modelBuilder.Entity<ArticleSummaryWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.DetailLink).HasMaxLength(255);

                entity.Property(e => e.Style).HasMaxLength(255);

                entity.Property(e => e.SubTitle).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ArticleSummaryWidget)
                    .HasForeignKey<ArticleSummaryWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleSummaryWidget_Widget");
            });

            modelBuilder.Entity<ArticleTopWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.ArticleTypeId).HasColumnName("ArticleTypeID");

                entity.Property(e => e.DetailPageUrl).HasMaxLength(255);

                entity.Property(e => e.MoreLink).HasMaxLength(255);

                entity.Property(e => e.SubTitle).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ArticleTopWidget)
                    .HasForeignKey<ArticleTopWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleTopWidget_Widget");
            });

            modelBuilder.Entity<ArticleType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Seodescription)
                    .HasColumnName("SEODescription")
                    .HasMaxLength(300);

                entity.Property(e => e.SeokeyWord)
                    .HasColumnName("SEOKeyWord")
                    .HasMaxLength(100);

                entity.Property(e => e.Seotitle)
                    .HasColumnName("SEOTitle")
                    .HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(100);
            });

            modelBuilder.Entity<ArticleTypeWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.ArticleTypeId).HasColumnName("ArticleTypeID");

                entity.Property(e => e.TargetPage).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ArticleTypeWidget)
                    .HasForeignKey<ArticleTypeWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleTypeWidget_Widget");
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PromoCode).HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Carousel>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<CarouselItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CarouselId).HasColumnName("CarouselID");

                entity.Property(e => e.CarouselWidgetId)
                    .HasColumnName("CarouselWidgetID")
                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.TargetLink).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.CarouselWidget)
                    .WithMany(p => p.CarouselItem)
                    .HasForeignKey(d => d.CarouselWidgetId)
                    .HasConstraintName("FK_CarouselItem_CarouselWidget");
            });

            modelBuilder.Entity<CarouselWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CarouselId).HasColumnName("CarouselID");

                entity.HasOne(d => d.Carousel)
                    .WithMany(p => p.CarouselWidget)
                    .HasForeignKey(d => d.CarouselId)
                    .HasConstraintName("FK_CarouselWidget_Carousel");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.CarouselWidget)
                    .HasForeignKey<CarouselWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarouselWidget_Widget");
            });

            modelBuilder.Entity<CmsLayout>(entity =>
            {
                entity.ToTable("CMS_Layout");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.ContainerClass).HasMaxLength(255);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.ImageThumbUrl).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.LayoutName).HasMaxLength(255);

                entity.Property(e => e.Script).HasMaxLength(255);

                entity.Property(e => e.Style).HasMaxLength(255);

                entity.Property(e => e.Theme).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<CmsLayoutHtml>(entity =>
            {
                entity.HasKey(e => e.LayoutHtmlId);

                entity.ToTable("CMS_LayoutHtml");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.LayoutId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PageId).HasMaxLength(100);

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.CmsLayoutHtml)
                    .HasForeignKey(d => d.LayoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CMS_LayoutHtml_CMS_Layout");
            });

            modelBuilder.Entity<CmsMedia>(entity =>
            {
                entity.ToTable("CMS_Media");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.ParentId)
                    .HasColumnName("ParentID")
                    .HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(100);
            });

            modelBuilder.Entity<CmsMessage>(entity =>
            {
                entity.ToTable("CMS_Message");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.PostMessage).IsRequired();

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<CmsPage>(entity =>
            {
                entity.ToTable("CMS_Page");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.LayoutId).HasMaxLength(100);

                entity.Property(e => e.MetaDescription).HasMaxLength(255);

                entity.Property(e => e.MetaKeyWorlds).HasMaxLength(255);

                entity.Property(e => e.PageName).HasMaxLength(100);

                entity.Property(e => e.ParentId).HasMaxLength(100);

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.ReferencePageId)
                    .HasColumnName("ReferencePageID")
                    .HasMaxLength(100);

                entity.Property(e => e.Script).HasMaxLength(255);

                entity.Property(e => e.Style).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.CmsPage)
                    .HasForeignKey(d => d.LayoutId)
                    .HasConstraintName("FK_CMS_Page_CMS_Layout");
            });

            modelBuilder.Entity<CmsRedirection>(entity =>
            {
                entity.ToTable("CMS_Redirection");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DestinationUrl)
                    .IsRequired()
                    .HasColumnName("DestinationURL")
                    .HasMaxLength(500);

                entity.Property(e => e.InComingUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<CmsRule>(entity =>
            {
                entity.HasKey(e => e.RuleId);

                entity.ToTable("CMS_Rule");

                entity.Property(e => e.RuleId).HasColumnName("RuleID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.RuleExpression).HasMaxLength(800);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.ZoneName).HasMaxLength(50);
            });

            modelBuilder.Entity<CmsTheme>(entity =>
            {
                entity.ToTable("CMS_Theme");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Thumbnail).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(100);

                entity.Property(e => e.UrlDebugger).HasMaxLength(100);
            });

            modelBuilder.Entity<CmsWidgetBase>(entity =>
            {
                entity.ToTable("CMS_WidgetBase");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.AssemblyName).HasMaxLength(255);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.FormView).HasMaxLength(255);

                entity.Property(e => e.IsSystem).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTemplate).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.LayoutId).HasMaxLength(100);

                entity.Property(e => e.PageId).HasMaxLength(100);

                entity.Property(e => e.PartialView).HasMaxLength(255);

                entity.Property(e => e.RuleId).HasColumnName("RuleID");

                entity.Property(e => e.ServiceTypeName).HasMaxLength(255);

                entity.Property(e => e.StyleClass).HasMaxLength(1000);

                entity.Property(e => e.Thumbnail).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.ViewModelTypeName).HasMaxLength(255);

                entity.Property(e => e.WidgetName).HasMaxLength(255);

                entity.Property(e => e.ZoneId).HasMaxLength(100);
            });

            modelBuilder.Entity<CmsZone>(entity =>
            {
                entity.ToTable("CMS_Zone");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.HeadingCode).HasMaxLength(100);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.LayoutId).HasMaxLength(100);

                entity.Property(e => e.PageId).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.ZoneName).HasMaxLength(255);

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.CmsZone)
                    .HasForeignKey(d => d.LayoutId)
                    .HasConstraintName("FK_CMS_Zone_CMS_Layout");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CommentContent).HasMaxLength(500);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.PagePath).HasMaxLength(300);

                entity.Property(e => e.Picture).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(100);

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            modelBuilder.Entity<DataArchived>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<DataDictionary>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.DicName).HasMaxLength(255);

                entity.Property(e => e.DicValue).HasMaxLength(255);

                entity.Property(e => e.ImageThumbUrl).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<ExtendField>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.OwnerId)
                    .HasColumnName("OwnerID")
                    .HasMaxLength(100);

                entity.Property(e => e.OwnerModule).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<FormData>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.FormId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            modelBuilder.Entity<FormDataItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.FieldId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldText).HasMaxLength(1000);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.OptionValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Forms>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.NotificationReceiver).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<HtmlWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.Html).HasColumnName("HTML");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.HtmlWidget)
                    .HasForeignKey<HtmlWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HtmlWidget_Widget");
            });

            modelBuilder.Entity<ImageWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.AltText).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.Link).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ImageWidget)
                    .HasForeignKey<ImageWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageWidget_Widget");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => new { e.LanKey, e.CultureName });

                entity.Property(e => e.LanKey).HasMaxLength(190);

                entity.Property(e => e.CultureName).HasMaxLength(10);

                entity.Property(e => e.LanType).HasMaxLength(50);

                entity.Property(e => e.Module).HasMaxLength(50);
            });

            modelBuilder.Entity<MainListPartWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Navigation>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<NavigationWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.AlignClass).HasMaxLength(50);

                entity.Property(e => e.CustomerClass).HasMaxLength(255);

                entity.Property(e => e.Logo).HasMaxLength(255);

                entity.Property(e => e.RootId)
                    .HasColumnName("RootID")
                    .HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.NavigationWidget)
                    .HasForeignKey<NavigationWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NavigationWidget_Widget");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50);

                entity.Property(e => e.CompletePayTime).HasColumnType("datetime");

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.LogisticsCompany).HasMaxLength(50);

                entity.Property(e => e.PayTime).HasColumnType("datetime");

                entity.Property(e => e.PaymentGateway).HasMaxLength(50);

                entity.Property(e => e.PaymentId)
                    .HasColumnName("PaymentID")
                    .HasMaxLength(500);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Refund).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RefundDate).HasColumnType("datetime");

                entity.Property(e => e.RefundId)
                    .HasColumnName("RefundID")
                    .HasMaxLength(100);

                entity.Property(e => e.RefundReason).HasMaxLength(500);

                entity.Property(e => e.ShippingAddress).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TrackingNumber).HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PromoCode).HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PageView>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(50);

                entity.Property(e => e.KeyWords).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.PageTitle).HasMaxLength(200);

                entity.Property(e => e.PageUrl).HasMaxLength(500);

                entity.Property(e => e.Referer).HasMaxLength(1000);

                entity.Property(e => e.RefererName).HasMaxLength(255);

                entity.Property(e => e.SessionId)
                    .HasColumnName("SessionID")
                    .HasMaxLength(50);

                entity.Property(e => e.UserAgent).HasMaxLength(500);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => new { e.PermissionKey, e.RoleId })
                    .HasName("PK__Permission_PermissionKey_RoleId");

                entity.Property(e => e.PermissionKey).HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Module).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Permission_Role");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.BrandCd).HasColumnName("BrandCD");

                entity.Property(e => e.Color).HasMaxLength(255);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.ImageThumbUrl).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Norm).HasMaxLength(255);

                entity.Property(e => e.PartNumber).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.PurchasePrice).HasColumnType("money");

                entity.Property(e => e.RebatePrice).HasColumnType("money");

                entity.Property(e => e.Seodescription).HasColumnName("SEODescription");

                entity.Property(e => e.SeokeyWord)
                    .HasColumnName("SEOKeyWord")
                    .HasMaxLength(255);

                entity.Property(e => e.Seotitle)
                    .HasColumnName("SEOTitle")
                    .HasMaxLength(255);

                entity.Property(e => e.ShelfLife).HasMaxLength(255);

                entity.Property(e => e.SourceFrom).HasMaxLength(255);

                entity.Property(e => e.TargetFrom).HasMaxLength(255);

                entity.Property(e => e.TargetUrl).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Translator).HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .HasConstraintName("FK_Product_ProductCategory");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Seodescription)
                    .HasColumnName("SEODescription")
                    .HasMaxLength(300);

                entity.Property(e => e.SeokeyWord)
                    .HasColumnName("SEOKeyWord")
                    .HasMaxLength(100);

                entity.Property(e => e.Seotitle)
                    .HasColumnName("SEOTitle")
                    .HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(100);
            });

            modelBuilder.Entity<ProductCategoryTag>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductCategoryWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.TargetPage).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ProductCategoryWidget)
                    .HasForeignKey<ProductCategoryWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategoryWidget_Widget");
            });

            modelBuilder.Entity<ProductDetailWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerClass).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ProductDetailWidget)
                    .HasForeignKey<ProductDetailWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductDetailWidget_Widget");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<ProductListWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.Columns).HasMaxLength(255);

                entity.Property(e => e.DetailPageUrl).HasMaxLength(255);

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ProductListWidget)
                    .HasForeignKey<ProductListWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductListWidget_Widget");
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<ScriptWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ScriptWidget)
                    .HasForeignKey<ScriptWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScriptWidget_CMS_WidgetBase");
            });

            modelBuilder.Entity<SectionContent>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.SectionGroupId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SectionWidgetId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.SectionWidget)
                    .WithMany(p => p.SectionContent)
                    .HasForeignKey(d => d.SectionWidgetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SectionContent_Widget");
            });

            modelBuilder.Entity<SectionContentCallToAction>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.Href).HasMaxLength(255);

                entity.Property(e => e.InnerText).HasMaxLength(255);

                entity.Property(e => e.SectionWidgetId)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SectionContentImage>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.Href).HasMaxLength(255);

                entity.Property(e => e.ImageAlt).HasMaxLength(255);

                entity.Property(e => e.ImageSrc).HasMaxLength(255);

                entity.Property(e => e.ImageTitle).HasMaxLength(255);

                entity.Property(e => e.SectionWidgetId)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SectionContentParagraph>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.SectionWidgetId)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SectionContentTitle>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.Href).HasMaxLength(255);

                entity.Property(e => e.InnerText).HasMaxLength(255);

                entity.Property(e => e.SectionWidgetId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TitleLevel).HasMaxLength(10);
            });

            modelBuilder.Entity<SectionContentVideo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.SectionWidgetId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Thumbnail).HasMaxLength(200);

                entity.Property(e => e.Url).HasMaxLength(256);

                entity.Property(e => e.VideoTitle).HasMaxLength(200);
            });

            modelBuilder.Entity<SectionGroup>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.GroupName).HasMaxLength(255);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.PartialView).HasMaxLength(100);

                entity.Property(e => e.PercentWidth).HasMaxLength(100);

                entity.Property(e => e.SectionWidgetId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.PartialViewNavigation)
                    .WithMany(p => p.SectionGroup)
                    .HasForeignKey(d => d.PartialView)
                    .HasConstraintName("FK_SectionGroup_SectionGroup_Template");

                entity.HasOne(d => d.SectionWidget)
                    .WithMany(p => p.SectionGroup)
                    .HasForeignKey(d => d.SectionWidgetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SectionGroup_SectionWidget");
            });

            modelBuilder.Entity<SectionTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateName);

                entity.Property(e => e.TemplateName).HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ExampleData).HasMaxLength(100);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Thumbnail).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<SectionWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.SectionTitle).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.SectionWidget)
                    .HasForeignKey<SectionWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SectionWidget_CMS_WidgetBase");
            });

            modelBuilder.Entity<StyleSheetWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.StyleSheetWidget)
                    .HasForeignKey<StyleSheetWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StyleSheetWidget_CMS_WidgetBase");
            });

            modelBuilder.Entity<UserRoleRelation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleRelation)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRoleRelation_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleRelation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRoleRelation_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.ApiLoginToken).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Birthplace).HasMaxLength(255);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatebyName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EnglishName).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Hobby).HasMaxLength(100);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.LastUpdateBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdateByName).HasMaxLength(100);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.LoginIp)
                    .HasColumnName("LoginIP")
                    .HasMaxLength(50);

                entity.Property(e => e.MobilePhone).HasMaxLength(50);

                entity.Property(e => e.NickName).HasMaxLength(50);

                entity.Property(e => e.PassWord).HasMaxLength(255);

                entity.Property(e => e.PhotoUrl).HasMaxLength(255);

                entity.Property(e => e.Profession).HasMaxLength(50);

                entity.Property(e => e.Qq)
                    .HasColumnName("QQ")
                    .HasMaxLength(50);

                entity.Property(e => e.ResetToken).HasMaxLength(50);

                entity.Property(e => e.ResetTokenDate).HasColumnType("datetime");

                entity.Property(e => e.School).HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.UserTypeCd).HasColumnName("UserTypeCD");

                entity.Property(e => e.ZipCode).HasMaxLength(50);
            });

            modelBuilder.Entity<VideoWidget>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.VideoWidget)
                    .HasForeignKey<VideoWidget>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VideoWidget_CMS_WidgetBase");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
