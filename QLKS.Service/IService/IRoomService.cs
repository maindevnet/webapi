using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;

namespace QLKS.Service.IService
{
    public interface IRoomService
    {
        Task<Pageding<RoomsViewModel>> GetAll(Serachmodel serachmodel);

        Task<ResultMessage<RoomsViewModel>> GetById(int Id);

        Task<ResultMessage<bool>> Add(RoomsViewModel viewModel);

        Task<ResultMessage<bool>> Edit(RoomsViewModel viewModel);

        Task<ResultMessage<bool>> Delete(int Id);

        Task<ResultMessage<bool>> CheckName(string name);
    }
}