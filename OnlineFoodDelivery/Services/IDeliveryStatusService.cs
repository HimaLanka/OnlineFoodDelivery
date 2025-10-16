using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Services
{
    public interface IDeliveryStatusService
    {
        public DeliveryStatus AssignAgentToOrder(int orderId);
        //public DeliveryStatus UpdateDeliveryStatus(int deliveryId, string newStatus);
        Task<DeliveryStatus> UpdateDeliveryStatus(int deliveryId, string newStatus);
        public DeliveryStatus GetDeliveryById(int deliveryId);
        public List<DeliveryStatus> GetAllDeliveries();
        public Dictionary<string, int> GetMonthlyDeliveryCountByAgent(int agentId);
        public Dictionary<string, int> GetWeeklyDeliveryCountByAgent(int agentId);
    }
}
