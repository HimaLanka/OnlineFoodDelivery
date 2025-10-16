using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RestaurantId { get; set; }

        [Required(ErrorMessage = "Restaurant Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string ResName { get; set; }

        [Required(ErrorMessage = "Restaurant Image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Location Id is required")]
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public User Owner { get; set; }

        [Required(ErrorMessage = "Delivery Charges are required")]
        public decimal DeliveryCharges { get; set; }

        public ICollection<MenuCategory> MenuCategories { get; set; }
    }
}
