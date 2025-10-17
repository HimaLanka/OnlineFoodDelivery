using System.ComponentModel.DataAnnotations;

namespace OnlineFoodDelivery.DTOs
{
    public class LocationDto
    {
        [Required(ErrorMessage = "State Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string State { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

        [Required(ErrorMessage = "Area Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string Area { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be 6 digits")]
        public string Pincode { get; set; }
    }
}
