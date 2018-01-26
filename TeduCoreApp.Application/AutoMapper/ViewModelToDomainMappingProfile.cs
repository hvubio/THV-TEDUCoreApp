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

        }
    }
}
