using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace QLKS.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
       
        private DbContext _dbContext;
        public UnitOfWork(DbContext context)
        {
            this._dbContext = context;
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}