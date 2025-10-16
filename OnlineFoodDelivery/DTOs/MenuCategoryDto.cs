using System.ComponentModel.DataAnnotations;

namespace OnlineFoodDelivery.DTOs
{
    public class MenuCategoryDto
    {
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(20, MinimumLength = 3)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Restaurant Id is required")]
        public long RestaurantId { get; set; }
        
    }
}
