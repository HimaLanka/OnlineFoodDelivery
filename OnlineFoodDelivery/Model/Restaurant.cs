using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class Restaurant 
    {
        [Key]
        public int RestaurantId { get; set; }

        [Required]
        public string? ResName { get; set; }

        public decimal DeliveryCharges { get; set; }

        public  ICollection<MenuCategory>? MenuCategories { get; set; }
    }
}
