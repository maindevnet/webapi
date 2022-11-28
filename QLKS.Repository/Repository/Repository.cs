using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QLKS.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _dataContext;
        protected Repository(DbContext context)
        {
            _dataContext = context;
        }

        public void Created(T entity)
        {
            _dataContext.Set<T>().Add(entity);
        }

        public void Updated(T entity)
        {
            _dataContext.Set<T>().Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            var entity = _dataContext.Set<T>().Find(Id);
            _dataContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _dataContext.Set<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> Contains(Expression<Func<T, bool>> expression)
        {
            return await _dataContext.Set<T>().AnyAsync(expression);
        }

        public async Task<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            return await _dataContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetListByExpression(Expression<Func<T, bool>> expression)
        {
            return _dataContext.Set<T>().Where(expression);
        }

        public async Task DeleteByExpression(Expression<Func<T, bool>> expression)
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(expression);
            _dataContext.Set<T>().Remove(entity);
        }
    }
}