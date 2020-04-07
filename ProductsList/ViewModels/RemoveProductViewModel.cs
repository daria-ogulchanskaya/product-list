using System.ComponentModel.DataAnnotations;

namespace ProductsList.ViewModels
{
    public class RemoveProductViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
