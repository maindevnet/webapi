using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System.Data.Entity;

namespace QLKS.Repository.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbFactory) : base(dbFactory)
        {
        }
    }
}