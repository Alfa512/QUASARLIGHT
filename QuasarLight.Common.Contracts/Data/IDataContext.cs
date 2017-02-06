using System;
using QuasarLight.Common.Contracts.Repositories;

namespace QuasarLight.Common.Contracts.Data
{
    public interface IDataContext : IDisposable
    {
        void Commit();
        IUserRepository Users { get; }
        IUserImageRepository UserImages { get; }
        IImageRepository Images { get; }
    }
}