using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class CartItem
    {
        [Key]
        public long CartItemId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [Required]
        public long MenuItemId { get; set; }

        [ForeignKey("MenuItemId")]
        public virtual MenuItem? MenuItem { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.Now;

        public int? OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }
}
