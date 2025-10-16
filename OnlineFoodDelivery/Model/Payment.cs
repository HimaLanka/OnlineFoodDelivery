using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{

    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public PaymentMethod Method { get; set; }

        [StringLength(20)]
        public string? CardType { get; set; }

        [StringLength(16)]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be 16 digits")]
        public string? CardNumber { get; set; }

        [StringLength(30)]
        public string? UpiProvider { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^\w+@\w+$", ErrorMessage = "Invalid UPI ID format")]
        public string? UpiId { get; set; }

        public bool IsCashOnDelivery { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public bool IsSuccess { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }

    public enum PaymentMethod
    {
        Card,
        UPI,
        CashOnDelivery
    }
}