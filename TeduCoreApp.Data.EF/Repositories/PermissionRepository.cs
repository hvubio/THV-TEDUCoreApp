using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class PermissionRepository: EfRepository<Permission,int>, IPermissionRepository
    {
        private readonly AppDbContext _context;
        public PermissionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
