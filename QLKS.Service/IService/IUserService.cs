using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;

namespace QLKS.Service.IService
{
    public interface IUserService
    {
        Task<Pageding<UsersViewModel>> GetAll(Serachmodel serachmodel);

        Task<ResultMessage<UsersViewModel>> GetById(int Id);

        Task<ResultMessage<bool>> Add(UsersViewModel viewModel);

        Task<ResultMessage<bool>> Edit(UsersViewModel viewModel);

        Task<ResultMessage<bool>> Delete(int Id);

        Task<ResultMessage<bool>> CheckName(string name);
        Task<ResultMessage<UsersViewModel>> Login(string userName,string password);
    }
}