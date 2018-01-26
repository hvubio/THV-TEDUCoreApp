using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Application.ViewModels.Product
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }

        [Required] [StringLength(255)] public string Name { get; set; }

        [Required] [StringLength(500)] public string Description { get; set; }

        public int? ParentId { get; set; }
        public int? HomeOrder { get; set; }

        [Required] [StringLength(255)] public string Image { get; set; }

        public bool HomeFlag { get; set; }

        // link key product
        public ICollection<ProductViewModel> Products { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public int SortOrder { get; set; }
        public Status Status { get; set; }
    }
}