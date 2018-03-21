using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;

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

        #endregion
    }
}