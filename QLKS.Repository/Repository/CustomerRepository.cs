using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System.Data.Entity;

namespace QLKS.Repository.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext dbFactory):base(dbFactory)
        {

        }
    }
}