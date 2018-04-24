using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(m=> new ProductCategory(m.Name, m.Description, m.ParentId, m.HomeOrder, m.Image, m.HomeFlag, m.SeoPageTitle,
                    m.SeoAlias, m.SeoKeywords, m.SeoDescription, m.SortOrder, m.Status));
            CreateMap<ProductViewModel, Product>().ConstructUsing(m => new Product(m.Name,m.Image,m.Price,m.PromotionPrice, m.OriginalPrice, m.Description,m.Content,m.HomeFlag,
                m.NewFlag, m.HotFlag,m.ViewCount,m.Tag,m.Unit, m.SeoPageTitle, m.SeoAlias, m.SeoKeywords, m.SeoDescription, m.Status,m.CategoryId));

        }
    }
}
