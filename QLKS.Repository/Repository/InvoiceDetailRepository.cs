using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using System;
using System.Data.Entity;
using System.Linq.Expressions;

namespace QLKS.Repository.Repository
{
    public class InvoiceDetailRepository : Repository<InvoiceDetail>, IInvoiceDetailRepository
    {
        public InvoiceDetailRepository(DbContext dbFactory) : base(dbFactory)
        {
        }
    }
}