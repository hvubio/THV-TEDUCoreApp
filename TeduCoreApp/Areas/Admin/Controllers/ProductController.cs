using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;

namespace TeduCoreApp.Areas.Admin.Controllers
{

    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API Ajax
        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _productService.GetAll();
            return new OkObjectResult(model);
        }

        #endregion
    }
}