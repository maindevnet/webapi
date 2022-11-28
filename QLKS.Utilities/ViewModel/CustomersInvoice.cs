using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLKS.Utilities.ViewModel
{
    public class CustomersInvoice
    {
        //Phan khach hang //
        public int Id { get; set; }

        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string DateofBirth { get; set; }
        public Nullable<bool> Sex { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfHire { get; set; }
        public string DateofHire { get; set; }
        public Nullable<bool> Status { get; set; }

        //Phan hoa don
        public int Hoadon_Id { get; set; }

        public Nullable<int> NV_Id { get; set; }
        public Nullable<decimal> Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfPayment { get; set; }
        public string DateofPayment { get; set; }
        public Nullable<int> Rooms_Id { get; set; }

        /// san pham//
        public List<InvoiceDetailViewModel> InvoiceDetails { get; set; }
    }
}