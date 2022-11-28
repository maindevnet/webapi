using System;
using System.Threading.Tasks;

namespace QLKS.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        void Commit();
        Task CommitAsync();
    }
}