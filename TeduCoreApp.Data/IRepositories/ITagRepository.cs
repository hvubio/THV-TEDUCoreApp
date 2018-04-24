using System;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Data.IRepositories
{
    public interface ITagRepository: IRepository<Tag,String>
    {
    }
}
