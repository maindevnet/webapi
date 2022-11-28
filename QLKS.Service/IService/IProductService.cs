using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;

namespace QLKS.Service.IService
{
    public interface IProductService
    {
        Task<Pageding<ProductsViewModel>> GetAll(Serachmodel serachmodel);

        Task<ResultMessage<ProductsViewModel>> GetById(int Id);

        Task<ResultMessage<bool>> Add(ProductsViewModel viewModel);

        Task<ResultMessage<bool>> Edit(ProductsViewModel viewModel);

        Task<ResultMessage<bool>> Delete(int Id);

        Task<ResultMessage<bool>> CheckName(string name);
    }
}