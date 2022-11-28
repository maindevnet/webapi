using System;

namespace QLKS.Utilities.ViewModel
{
    public class CustomersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<bool> Sex { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }
        public Nullable<System.DateTime> DateOfHire { get; set; }
        public Nullable<bool> Status { get; set; }
       
        //public virtual InvoiceViewModel Invoice { get; set; }
       // public virtual RoomsViewModel Room { get; set; }
    }
}