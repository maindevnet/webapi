using System;
using System.ComponentModel.DataAnnotations;

namespace QLKS.Utilities.ViewModel
{
    public class ProductsViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public Nullable<decimal> Price { get; set; }
        public string Unit { get; set; }
        public Nullable<int> TypeProducts_Id { get; set; }
        public virtual TypeProductsViewModel TypeProduct { get; set; }
    }
}