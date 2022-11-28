using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;

namespace QLKS.Service.IService
{
    public interface IKindOfRoomService
    {
        Task<Pageding<KindOfRoomsViewModel>> GetAll(Serachmodel serachmodel);
        Task<ResultMessage<KindOfRoomsViewModel>> GetById(int Id);
        Task<ResultMessage<bool>> Add(KindOfRoomsViewModel kindOfRooms);
        Task<ResultMessage<bool>> Edit(KindOfRoomsViewModel kindOfRooms);
        Task<ResultMessage<bool>> Delete(int Id);
        Task<ResultMessage<bool>> CheckName(string name);
    }
}