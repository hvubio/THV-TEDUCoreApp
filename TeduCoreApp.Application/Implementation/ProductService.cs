using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.Utilities.Helpers;

namespace TeduCoreApp.Application.Implementation
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductTagRepository _productTagRepository;
        private readonly ITagRepository _tagRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ITagRepository tagRepository, IProductTagRepository productTagRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ProductViewModel> GetAll()
        {
            return _productRepository.FindAll(x=>x.ProductCategory).ProjectTo<ProductViewModel>().ToList();
        }

        public PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            int totalRow = query.Count();

            query = query.OrderByDescending(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);

            var data = query.ProjectTo<ProductViewModel>().ToList();

            var paginationSet = new PagedResult<ProductViewModel>()
            {
                CurrentPage = page,
                RowCount = totalRow,
                Results = data,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public ProductViewModel Add(ProductViewModel productViewModel)
        {
           var productTags = new List<ProductTag>();

            if (!string.IsNullOrEmpty(productViewModel.Tag))
            {
                string[] tags = productViewModel.Tag.Split(',');
                foreach (var t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x=>x.Id == tagId).Any())
                    {
                        Tag tag = new Tag()
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }

                    var productTag = new ProductTag()
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
            }
            var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
            foreach (var productTag in productTags)
            {
                product.ProductTags.Add(productTag);
            }
            _productRepository.Add(product);

            return productViewModel;
        }

        public void Update(ProductViewModel productViewModel)
        {
            var productTags = new List<ProductTag>();

            if (!string.IsNullOrEmpty(productViewModel.Tag))
            {
                _productTagRepository.RemoveMultiple(_productTagRepository.FindAll(x => x.ProductId == productViewModel.Id).ToList());
                string[] tags = productViewModel.Tag.Split(',');
                foreach (var t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag()
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }

                    var productTag = new ProductTag()
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
            }
            else
            {
                _productTagRepository.RemoveMultiple(_productTagRepository.FindAll(x=>x.ProductId == productViewModel.Id).ToList());
            }
            var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
            foreach (var productTag in productTags)
            {
                product.ProductTags.Add(productTag);
            }

            _productRepository.Update(product);
        }

        public void Delete(int id)
        {
            _productRepository.Remove(id);
        }

        public ProductViewModel GetById(int id)
        {
            return Mapper.Map<Product, ProductViewModel>(_productRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
