using System;

namespace TeduCoreApp.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Call SaveChange from db context
        /// </summary>
        void Commit();
    }
}