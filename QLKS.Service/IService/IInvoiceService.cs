using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System.Threading.Tasks;

namespace QLKS.Service.IService
{
    public interface IInvoiceService
    {
        Task<Pageding<InvoiceViewModel>> GetAll(Serachmodel serachmodel);

        Task<ResultMessage<InvoiceViewModel>> GetById(int Id);

        Task<ResultMessage<bool>> Add(InvoiceViewModel Customer);

        Task<ResultMessage<bool>> Edit(InvoiceViewModel Customer);

        Task<ResultMessage<bool>> Delete(int Id);
    }
}