using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System.Data.Entity;

namespace QLKS.Repository.Repository
{
    public class TypeProductRepository : Repository<TypeProduct>, ITypeProductRepository
    {
        public TypeProductRepository(DbContext dbFactory) : base(dbFactory)
        {
        }
    }
}