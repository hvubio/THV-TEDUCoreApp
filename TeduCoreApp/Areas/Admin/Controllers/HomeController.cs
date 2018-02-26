using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Extensions;

namespace TeduCoreApp.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var email = User.GetSpecificClaim("Email");
            return View();
        }
    }
}