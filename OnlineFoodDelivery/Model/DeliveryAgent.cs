using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class DeliveryAgent
    {
        [Key]
        public int AgentId { get; set; }
        public int UserId { get; set; }

        [Required]
        public string AgentStatus { get; set; }  // Available, Busy, Offline

        [Required]
        public string VehicleNumber { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
