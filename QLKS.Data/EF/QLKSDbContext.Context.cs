﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLKS.Data.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLKSEntities : DbContext
    {
        public QLKSEntities()
            : base("name=QLKSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<KindOfRoom> KindOfRooms { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<TypeProduct> TypeProducts { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
