using System.ComponentModel.DataAnnotations;

namespace OnlineFoodDelivery.DTOs
{
    public class RestaurantDto
    {
        [Required(ErrorMessage = "Restaurant Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string ResName { get; set; }

        [Required(ErrorMessage = "Restaurant Image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Location Id is required")]
        public int LocationId { get; set; }
    }

}
