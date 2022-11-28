using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System.Data.Entity;

namespace QLKS.Repository.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(DbContext dbFactory) : base(dbFactory)
        {
        }
    }
}