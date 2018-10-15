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
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Implementation
{
    public class UserService: IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<bool> AddAsync(AppUserViewModel userVm)
        {
            var user = new AppUser()
            {
                UserName = userVm.UserName,
                Avatar = userVm.Avatar,
                Email = userVm.Email,
                FullName = userVm.FullName,
                DateCreated = userVm.DateCreated,
                PhoneNumber = userVm.PhoneNumber,
                Status = userVm.Status
            };
            var result = await _userManager.CreateAsync(user, userVm.Password);
            if (result.Succeeded && userVm.Roles.Count>0 )
            {
                var appUser = await _userManager.FindByNameAsync(user.UserName);
                if (appUser != null)
                {
                    await _userManager.AddToRolesAsync(appUser, userVm.Roles);
                    
                }

            }

            return true;

        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<List<AppUserViewModel>> GetAllAsync()
        {
            return await _userManager.Users.ProjectTo<AppUserViewModel>().ToListAsync();
        }

        public PagedResult<AppUserViewModel> GetAllPageAsync(string keyword, int page, int pageSize)
        {
            var query = _userManager.Users;
            if (!String.IsNullOrEmpty(keyword))
            {
                query =  query.Where(x =>
                    x.UserName.Contains(keyword) || x.FullName.Contains(keyword) || x.Email.Contains(keyword));
                
            }

            var totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var data = query.Select(x => new AppUserViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Avatar = x.Avatar,
                BirthDay = x.Brithday,
                DateCreated = x.DateCreated,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Status = x.Status

            }).ToList();

            var paginationSet = new PagedResult<AppUserViewModel>()
            {
                Results = data,
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = totalRow
            };

            return paginationSet;

        }

        public async Task<AppUserViewModel> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);
            var userVm = Mapper.Map<AppUser, AppUserViewModel>(user);
            userVm.Roles = role.ToList();
            return userVm;

        }

        public async Task UpdateAsync(AppUserViewModel userVm)
        {
            var user = await _userManager.FindByIdAsync(userVm.Id.ToString());

            // remove current roles
            var currentRole = await _userManager.GetRolesAsync(user);
            var result = await _userManager.AddToRolesAsync(user, userVm.Roles.Except(currentRole));

            if (result.Succeeded)
            {
                var needRemoveRole = currentRole.Except(userVm.Roles).ToArray();
                await _userManager.RemoveFromRolesAsync(user, needRemoveRole); // dong code nay bi loi vi khong tim thay role trong user can xoa 

                // Update user detail
                user.FullName = userVm.FullName;
                user.Status = userVm.Status;
                user.PhoneNumber = userVm.PhoneNumber;
                user.Email = userVm.Email;
                await _userManager.UpdateAsync(user);
            }
            
        }
    }
}
