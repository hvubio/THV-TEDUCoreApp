﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Azure.KeyVault.Models;
using TeduCoreApp.Application.Implementation;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.System;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _roleService.GetAllAsync();
            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _roleService.GetById(id);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _roleService.DeleteAsync(id);
            return new OkObjectResult(id);
        }

        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _roleService.GetAllPagingAsync(keyword, page, pageSize);
            return  new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(AppRoleViewModel roleVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(e => e.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (roleVm.Id == null)
                {
                    await _roleService.AddAsync(roleVm);
                   
                }
                else
                {
                    await _roleService.UpdateAsync(roleVm);
                    
                }
                return new OkObjectResult(roleVm);
            }
        }
        [HttpPost]
        public IActionResult ListAllFunction(Guid roleId)
        {
            var function = _roleService.GetListFunctionWithRole(roleId);
            return new OkObjectResult(function);
        }

        [HttpPost]
        public IActionResult SavePermission(List<PermissionViewModel> listPermission, Guid roleId)
        {
            _roleService.SavePermission(listPermission,roleId);
            return new OkResult();
        }
    }
}