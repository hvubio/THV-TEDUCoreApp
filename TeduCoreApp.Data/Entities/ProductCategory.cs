﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    public class ProductCategory : DomainEntity<int>, IHasSeoMetaData, ISwitchable, ISortable, IDateTracking
    {
        public ProductCategory()
        {
            Products = new List<Product>(); // make this for Product not null
        }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int? HomeOrder { get; set; }

        [Required]
        [StringLength(255)]
        public string Image { get; set; }
        public bool HomeFlag { get; set; }

        // link key product
        public virtual ICollection<Product> Products { get; set; }

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