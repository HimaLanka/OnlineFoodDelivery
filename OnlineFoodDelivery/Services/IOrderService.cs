using System;
using System.Collections.Generic;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Services
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int orderId);
        List<Order> GetOrdersByUserId(int userId);
        List<Order> GetOrdersByRestaurantId(int restaurantId);
        List<Order> GetOrdersByDateRange(DateTime start, DateTime end);
        Order PlaceOrder(int userId);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
        bool OrderExists(int orderId);
        Order PlaceOrder(Order order);

    }
}
