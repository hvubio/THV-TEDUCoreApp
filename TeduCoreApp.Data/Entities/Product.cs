using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Product")]
    public class Product : DomainEntity<int>, IHasSeoMetaData, ISwitchable, IDateTracking
    {
        public Product(string name, string image, decimal price, decimal? promotionPrice, decimal? originalPrice, string description, string content,
            bool? homeFlag, bool newFlag, bool hotFlag, int? viewCount, string tags, string unit, string seoPageTitle, string seoAlias, 
            string seoKeywords, string seoDescription, Status status, int categoryId)
        {
            Name = name;
            Image = image;
            Price = price;
            PromotionPrice = promotionPrice;
            OriginalPrice = originalPrice;
            Description = description;
            Content = content;
            CategoryId = CategoryId;
            HomeFlag = homeFlag;
            NewFlag = newFlag;
            HotFlag = hotFlag;
            ViewCount = viewCount;
            Tag = tags;
            Unit = unit;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
            Status = status;
            ProductTags = new List<ProductTag>();
        }

        public Product()
        {
        }

        [StringLength(255)] [Required] public string Name { get; set; }

        [StringLength(255)] [Required] public string Image { get; set; }

        [Required] [DefaultValue(0)] public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }
        public decimal? OriginalPrice { get; set; }

        [StringLength(255)] public string Description { get; set; }

        [Required] public string Content { get; set; }

        public bool? HomeFlag { get; set; }
        public bool NewFlag { get; set; }
        public bool HotFlag { get; set; }
        public int? ViewCount { get; set; }
        
        public string Tag { get; set; }
        public string Unit { get; set; }
        public int CategoryId { get; set; }

        //link Foreign key
        [ForeignKey("CategoryId")] public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<ProductColor> ProductColors { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public Status Status { get; set; }
    }
}