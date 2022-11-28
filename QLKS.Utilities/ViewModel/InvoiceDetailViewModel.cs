using System;

namespace QLKS.Utilities.ViewModel
{
    public class InvoiceDetailViewModel
    {
        public int Id { get; set; }
        public Nullable<int> Invoice_Id { get; set; }
        public Nullable<int> Product_Id { get; set; }
        public string Names { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<System.DateTime> DateService { get; set; }
        public Nullable<bool> Status { get; set; }

        public virtual ProductsViewModel Product { get; set; }
    }
}