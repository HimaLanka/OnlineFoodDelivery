using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Services
{
    public interface IAgentService
    {
        public List<DeliveryAgent> GetAllAgents();
        public DeliveryAgent GetAgentById(int id);
        public int AddAgent(DeliveryAgent product);
        public int UpdateAgent(int id, DeliveryAgent product);
        public int DeleteAgent(int id);
    }
}
