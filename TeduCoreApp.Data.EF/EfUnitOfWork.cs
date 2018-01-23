using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Data.EF
{
    public class EfUnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        public EfUnitOfWork(AppDbContext context)
        {
            this._context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
