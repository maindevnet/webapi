using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System.Data.Entity;

namespace QLKS.Repository.Repository
{
    public class KindOfRoomRepository: Repository<KindOfRoom>, IKindOfRoomRepository
    {
        public KindOfRoomRepository(DbContext context) : base(context)
        {
        }
    }
}