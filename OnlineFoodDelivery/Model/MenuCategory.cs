using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class MenuCategory
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(20, MinimumLength = 3)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Restaurant Id is required")]
        public long RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}


