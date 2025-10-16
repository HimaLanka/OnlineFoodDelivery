using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class MenuCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        public int RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant? Restaurant { get; set; }

        public ICollection<MenuItem>? MenuItems { get; set; }
    }
}


