using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.System;
using TeduCoreApp.Extensions;
using TeduCoreApp.Utilities.Constants;

namespace TeduCoreApp.Areas.Admin.Components
{
    public class SideBarViewComponent: ViewComponent
    {
        private readonly IFunctionService _functionService;

        public SideBarViewComponent(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal) User).GetSpecificClaim("Roles");

            List<FunctionViewModel> functionViewModels;

            if (roles.Split(";").Contains(CommonConstants.AdminRole))
            {
                functionViewModels = await _functionService.GetAll(string.Empty);
            }
            else
            {
                //TODO: Get by Permission
                functionViewModels = new List<FunctionViewModel>();
            }

            return View(functionViewModels);
        }
    }
}
