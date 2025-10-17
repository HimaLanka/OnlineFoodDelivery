using OnlineFoodDelivery.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.DTOs
{
    public class MenuItemDto
    {
        [Required(ErrorMessage = "Item Name is required")]
        [StringLength(30, MinimumLength = 3)]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Item Price is required")]
        public decimal ItemPrice { get; set; }
        public string ItemDescription { get; set; }
        public string ItemImg { get; set; }

        [Required(ErrorMessage = "Mention (true/false)")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Mention (true/false)")]
        public bool IsVeg { get; set; }

        [Required(ErrorMessage = "Category Id is required")]
        public long CategoryId { get; set; }

    }
}
