using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;

namespace QLKS.Service.IService
{
    public interface ITypeProductService
    {
        Task<Pageding<TypeProductsViewModel>> GetAll(Serachmodel serachmodel);

        Task<ResultMessage<TypeProductsViewModel>> GetById(int Id);

        Task<ResultMessage<bool>> Add(TypeProductsViewModel viewModel);

        Task<ResultMessage<bool>> Edit(TypeProductsViewModel viewModel);

        Task<ResultMessage<bool>> Delete(int Id);

        Task<ResultMessage<bool>> CheckName(string name);
    }
}