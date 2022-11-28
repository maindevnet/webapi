using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;

namespace QLKS.Service.IService
{
    public interface ICustomerService
    {
        Task<Pageding<CustomersViewModel>> GetAll(Serachmodel serachmodel);

        Task<ResultMessage<CustomersInvoice>> GetById(int Id);

        Task<ResultMessage<bool>> Add(CustomersInvoice Customer);

        Task<ResultMessage<bool>> Edit(CustomersInvoice Customer);

        Task<ResultMessage<bool>> Delete(int Id);

        Task<ResultMessage<bool>> CheckName(string name);
    }
}