using System.Collections.Generic;
using System;

namespace QLKS.Utilities.ViewModel
{
    public class RoomsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<int> KindOfRooms_Id { get; set; }

        public virtual KindOfRoomsViewModel KindOfRoom { get; set; }
    }
}