using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System.Data.Entity;

namespace QLKS.Repository.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(DbContext dbFactory) : base(dbFactory)
        {
        }
    }
}