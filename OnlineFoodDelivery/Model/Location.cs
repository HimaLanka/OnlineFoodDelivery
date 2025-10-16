using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }

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
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
