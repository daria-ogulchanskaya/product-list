using ProductsList.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductsList.ViewModels
{
    public class UpdateProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public string Amount { get; set; }

        [Required]
        public Unit Unit { get; set; }
    }
}
