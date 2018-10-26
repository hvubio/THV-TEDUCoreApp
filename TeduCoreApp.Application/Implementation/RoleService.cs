using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.System;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Implementation
{
    public class RoleService: IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFunctionRepository _functionRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RoleService(RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork, IFunctionRepository functionRepository, 
            IPermissionRepository permissionRepository)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _functionRepository = functionRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<bool> AddAsync(AppRoleViewModel roleVm)
        {
            var role = new AppRole()
            {
                Name = roleVm.Name,
                Description = roleVm.Description
            };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var result = await _roleManager.DeleteAsync(role);
            
        }

        public async Task<List<AppRoleViewModel>> GetAllAsync()
        {
            return await _roleManager.Roles.ProjectTo<AppRoleViewModel>().ToListAsync();
        }

        public PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));

            }

            var totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize); // query get 1 page data 

            var data = query.ProjectTo<AppRoleViewModel>().ToList();
            var paginationSet = new PagedResult<AppRoleViewModel>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };

            return paginationSet;

        }

        public async Task<AppRoleViewModel> GetById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return Mapper.Map<AppRole,AppRoleViewModel>(role);

        }

        public async Task UpdateAsync(AppRoleViewModel roleVm)
        {
            var role = await _roleManager.FindByIdAsync(roleVm.Id);
            role.Name = roleVm.Name;
            role.Description = roleVm.Description;
            await _roleManager.UpdateAsync(role);
        }

        public List<PermissionViewModel> GetListFunctionWithRole(Guid roleId)
        {
            var functions = _functionRepository.FindAll();
            var permissions = _permissionRepository.FindAll();

            var query = from f in functions
                join p in permissions on f.Id equals p.FunctionId into fp
                from p in fp.DefaultIfEmpty()
                where p != null && p.RoleId == roleId
                select new PermissionViewModel()
                {
                    RoleId = roleId,
                    FunctionId = f.Id,
                    CanCreate = p != null ? p.CanCreate : false,
                    CanDelete = p != null ? p.CanDelete : false,
                    CanRead = p != null ? p.CanRead : false,
                    CanUpdate = p != null ? p.CanUpdate : false
                };
            return query.ToList();
        }

        public void SavePermission(List<PermissionViewModel> permissionVm, Guid roleId)
        {
            var permissions = Mapper.Map<List<PermissionViewModel>,List<Permission>>(permissionVm);
            var oldPermission = _permissionRepository.FindAll().Where(x => x.RoleId == roleId).ToList();
            if (oldPermission.Count > 0)
            {
                _permissionRepository.RemoveMultiple(oldPermission);
            }

            foreach (var p in permissions)
            {
                _permissionRepository.Add(p);
            }

            _unitOfWork.Commit();           

        }

        public Task<bool> CheckPermission(string functionId, string action, string[] roles)
        {
            var functions = _functionRepository.FindAll();
            var permissions = _permissionRepository.FindAll();
            var query = from f in functions
                join p in permissions on f.Id equals p.FunctionId
                join r in _roleManager.Roles on p.RoleId equals r.Id
                where roles.Contains(r.Name) && f.Id == functionId
                                             && ((p.CanCreate && action == "Create")
                                                 || (p.CanUpdate && action == "Update")
                                                 || (p.CanDelete && action == "Delete")
                                                 || (p.CanRead && action == "Read"))
                select p;
            return query.AnyAsync();
        }
    }
}
