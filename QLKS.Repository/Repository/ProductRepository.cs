using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System.Data.Entity;

namespace QLKS.Repository.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext dbFactory) : base(dbFactory)
        {
        }
    }
}