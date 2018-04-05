using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Utilities.Helpers;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API Load Data
        [HttpGet]
        public IActionResult GetCategoryAll()
        {
            var mode = _productCategoryService.GetAll();
            return new OkObjectResult(mode);
        }

        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            var mode = _productCategoryService.GetProductCategoryById(id);
            return  new OkObjectResult(mode);
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductCategoryViewModel productCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                productCategoryViewModel.SeoAlias = TextHelper.ToUnsignString(productCategoryViewModel.Name);
                if (productCategoryViewModel.Id == 0)
                {
                    _productCategoryService.Add(productCategoryViewModel);
                }
                else
                {
                    _productCategoryService.Update(productCategoryViewModel);
                }
                _productCategoryService.Save(); 
                return new OkObjectResult(productCategoryViewModel);
            }

        }

        [HttpPost] // if this isn't Refuls API so this isn't need HttpDelete.
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new BadRequestResult();
            }
            else
            {
                _productCategoryService.Delete(id);
                _productCategoryService.Save();
                return new OkObjectResult(id);
            }
            
        }

        [HttpPost]
        public IActionResult UpdateParentId(int sourceId, int targetId, Dictionary<int,int> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _productCategoryService.UpdateParentId(sourceId, targetId, items);
                    _productCategoryService.Save();
                    return new OkResult();
                }
            }
            
        }

        [HttpPost]
        public IActionResult ReOrder(int sourceId, int targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else 
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _productCategoryService.ReOrder(sourceId, targetId);
                    _productCategoryService.Save();
                    return new OkResult();
                }
            }

            
        }
       

        #endregion
    }
}