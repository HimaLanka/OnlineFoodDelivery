using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class DeliveryAgent
    {
        [Key]
        public int AgentId { get; set; }
        public int Id { get; set; }

        [Required]
        public string AgentStatus { get; set; }  // Available, Busy, Offline

        [Required]
        public string VehicleNumber { get; set; }

        [ForeignKey("Id")]
        public User User { get; set; }
    }
}
