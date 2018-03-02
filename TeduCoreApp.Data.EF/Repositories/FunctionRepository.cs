using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class FunctionRepository: EfRepository<Function,string>, IFunctionRepository
    {
        private readonly AppDbContext _context;
        public FunctionRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
