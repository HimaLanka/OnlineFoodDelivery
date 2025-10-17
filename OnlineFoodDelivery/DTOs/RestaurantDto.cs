using OnlineFoodDelivery.Model;
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

        [Required(ErrorMessage = "User Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Delivery Charges are required")]
        public decimal DeliveryCharges { get; set; }
    }
}
