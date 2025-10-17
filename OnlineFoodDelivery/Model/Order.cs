using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class Order   
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]                    
        public virtual User User { get; set; }

        public long RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
