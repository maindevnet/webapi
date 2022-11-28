using QLKS.Utilities.BaseUtilites;
using QLKS.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKS.Service.IService
{
    public interface IInvoiceDetailService
    {
        Task<Pageding<InvoiceDetailViewModel>> GetAll(Serachmodel serachmodel);

        Task<ResultMessage<InvoiceDetailViewModel>> GetById(int Id);

        Task<ResultMessage<bool>> Add(InvoiceDetailViewModel Customer);

        Task<ResultMessage<bool>> Edit(InvoiceDetailViewModel Customer);

        Task<ResultMessage<bool>> Delete(int Id);
    }
}
