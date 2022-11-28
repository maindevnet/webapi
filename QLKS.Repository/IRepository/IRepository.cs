using QLKS.Data.EF;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QLKS.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Created(T entity);

        void Updated(T entity);

        void Delete(int Id);
        Task DeleteByExpression(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
        IQueryable<T> GetListByExpression(Expression<Func<T, bool>> expression);
        Task<T> GetById(int id);
        Task<T> GetByExpression(Expression<Func<T, bool>> expression);
        Task<bool> Contains(Expression<Func<T, bool>> expression);
    }
}