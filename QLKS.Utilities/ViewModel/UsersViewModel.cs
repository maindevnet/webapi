using System;

namespace QLKS.Utilities.ViewModel
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}