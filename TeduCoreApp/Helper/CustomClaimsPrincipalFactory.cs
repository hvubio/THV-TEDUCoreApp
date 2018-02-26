using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Helper
{
    public class CustomClaimsPrincipalFactory: UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        private readonly UserManager<AppUser> _userManager;

        public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim("FullName", user.FullName),
                new Claim("Email", user.Email),
                new Claim("Avatar", user.Avatar==null?"":String.Empty),
                new Claim("Role", string.Join(";",roles))
            });

            return principal;
        }
    }
}
