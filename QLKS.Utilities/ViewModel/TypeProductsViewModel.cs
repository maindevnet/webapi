using System.ComponentModel.DataAnnotations;

namespace QLKS.Utilities.ViewModel
{
    public class TypeProductsViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}