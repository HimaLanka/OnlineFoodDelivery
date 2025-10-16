
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Exceptions;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;

namespace OnlineFoodDelivery.Services
{
    public class DeliveryStatusService : IDeliveryStatusService
    {
        private readonly IDeliveryStatusRepository _repository;

        public DeliveryStatusService(IDeliveryStatusRepository repository)
        {
            _repository = repository;
        }

        public List<DeliveryStatus> GetAllDeliveries()
        {
            return _repository.GetAllDeliveries();
        }

        public DeliveryStatus GetDeliveryById(int deliveryId)
        {
            var delivery = _repository.GetDeliveryById(deliveryId);
            if (delivery == null)
            {
                throw new DeliveryByIDNotFoundException($"Delivery with ID {deliveryId} does not exist");
            }
            return delivery;
        }


        //public DeliveryStatus AssignAgentToOrder(int orderId)
        //{
        //    var delivery = _repository.AssignAgentToOrder(orderId);
        //    if (delivery == null)
        //    {
        //        throw new AgentsNotAvailableException($"Order with ID {orderId} could not be assigned to an agent due to unavailability of agents.");
        //    }
        //    return delivery;
        //}


        public DeliveryStatus AssignAgentToOrder(int orderId)
        {
            var delivery = _repository.AssignAgentToOrder(orderId);

            if (delivery == null)
            {
                throw new OrderNotFoundException($"Order with ID {orderId} does not exist");
            }

            if (delivery.OrderId == -1)
            {
                throw new AgentsNotAvailableException($"No available agents to assign for order ID {orderId}");
            }

            return delivery;
        }



        //public async Task<DeliveryStatus> UpdateDeliveryStatus(int deliveryId, string newStatus)
        //{
        //    var existingDelivery = _repository.GetDeliveryById(deliveryId);

        //    if (existingDelivery == null)
        //    {
        //        throw new DeliveryByIDNotFoundException($"Delivery with ID {deliveryId} does not exist");
        //    }

        //    return await _repository.UpdateDeliveryStatus(deliveryId, newStatus);
        //}

        public async Task<DeliveryStatus> UpdateDeliveryStatus(int deliveryId, string newStatus)
        {
            var delivery = await _repository.UpdateDeliveryStatus(deliveryId, newStatus);

            if (delivery == null)
                throw new DeliveryByIDNotFoundException($"Delivery with ID {deliveryId} does not exist");

            if (newStatus == "Delivered")
            {
                if (delivery.Order == null || delivery.Order.UserId == 0)
                    throw new CustomerNotFoundException("Order or customer information is missing");

                if (delivery.Order.User == null)
                    throw new CustomerNotFoundException("Customer not found in delivery data");
            }

            return delivery;
        }






        public Dictionary<string, int> GetMonthlyDeliveryCountByAgent(int agentId)
        {
            var deliveryCounts = _repository.GetMonthlyDeliveryCountByAgent(agentId);

            if (deliveryCounts == null || !deliveryCounts.Any())
            {
                throw new AgentsNotAvailableException($"Agent with ID {agentId} does not exist or has no deliveries");
            }

            return deliveryCounts;
        }


        public Dictionary<string, int> GetWeeklyDeliveryCountByAgent(int agentId)
        {
            var deliveryCountsWeekly = _repository.GetWeeklyDeliveryCountByAgent(agentId);

            if (deliveryCountsWeekly == null || !deliveryCountsWeekly.Any())
            {
                throw new AgentsNotAvailableException($"Agent with ID {agentId} does not exist or has no deliveries");
            }
            return deliveryCountsWeekly;
        }
    }
}



























        //public DeliveryStatus AssignAgentToOrder(int orderId)
        //{
        //    return _repository.AssignAgentToOrder(orderId);
        //}

        //public async Task<DeliveryStatus> UpdateDeliveryStatus(int deliveryId, string newStatus)
        //{
        //    return await _repository.UpdateDeliveryStatus(deliveryId, newStatus);
        //}



        //public DeliveryStatus UpdateDeliveryStatus(int deliveryId, string newStatus)
        //{
        //    return _repository.UpdateDeliveryStatus(deliveryId, newStatus);
        //}
