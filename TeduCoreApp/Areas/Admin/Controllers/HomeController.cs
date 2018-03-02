using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Extensions;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Authorize]
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