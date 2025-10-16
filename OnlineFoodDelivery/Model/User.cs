using System.ComponentModel.DataAnnotations;

namespace OnlineFoodDelivery      
{
    public class User
    {
        [Key]

        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]

        [StringLength(50, MinimumLength = 3)]

        public string? Username { get; set; }

        [Required]

        [EmailAddress(ErrorMessage = "Enter a valid email address")]

        public string? Email { get; set; }

        [Required]

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone must be 10 digits")]

        [DataType(DataType.PhoneNumber)]

        public long PhoneNumber { get; set; }

        [Required]

        [DataType(DataType.Password)]

        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]

        [DataType(DataType.Password)]

        public string? ConfirmPassword { get; set; }

        public string? BankDetails { get; set; }

        [MaxLength(200)]

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]

        public string? Role { get; set; }

    }

}

