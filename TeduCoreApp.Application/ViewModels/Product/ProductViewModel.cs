using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Application.ViewModels.Product
{
    public class ProductViewModel
    {
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
        public ProductCategoryViewModel ProductCategory { get; set; }

        public ICollection<ProductColor> ProductColors { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public Status Status { get; set; }
    }
}