using System.Collections.Generic;
using System;

namespace QLKS.Utilities.ViewModel
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public Nullable<int> NV_Id { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> DateOfPayment { get; set; }
        public Nullable<int> Rooms_Id { get; set; }
        public Nullable<int> Customers_Id { get; set; }

       /// public virtual ICollection<CustomersViewModel> Customers { get; set; }
        
        public virtual ICollection<InvoiceViewModel> InvoiceDetails { get; set; }
       /// public virtual RoomsViewModel Room { get; set; }
    }
}