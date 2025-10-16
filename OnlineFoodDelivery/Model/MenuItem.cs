using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class MenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuItemId { get; set; }

        [Required]
        public string? ItemName { get; set; }

        [Required]
        public decimal ItemPrice { get; set; }
        public string? ItemDescription { get; set; }  

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public bool IsVeg { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public MenuCategory? Category { get; set; }
    }
}
