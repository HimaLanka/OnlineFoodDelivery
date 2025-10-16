using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Model
{
    public class DeliveryStatus
    {
        [Key]
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public int AgentId { get; set; }
        public string StatusOfDelivey {  get; set; }    // In Progress, Delivered
        public DateTime EstimatedTimeOfArrival { get; set; }

        [ForeignKey("AgentId")]
        public DeliveryAgent Agent { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}