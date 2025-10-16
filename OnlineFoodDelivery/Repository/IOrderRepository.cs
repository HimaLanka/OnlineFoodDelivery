using System;
using System.Collections.Generic;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int orderId);
        List<Order> GetOrdersByUserId(int userId);
        List<Order> GetOrdersByRestaurantId(int restaurantId);
        List<Order> GetOrdersByDateRange(DateTime start, DateTime end);
        Order CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
        bool OrderExists(int orderId);
    }
}

