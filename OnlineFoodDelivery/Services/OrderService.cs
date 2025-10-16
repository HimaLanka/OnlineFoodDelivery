using System;
using System.Collections.Generic;
using System.Linq;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;
using OnlineFoodDelivery.Exceptions;

namespace OnlineFoodDelivery.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public List<Order> GetAllOrders() => _orderRepository.GetAllOrders();

        public Order GetOrderById(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
                throw new OrderNotFoundException($"Order with ID {orderId} not found.");
            return order;
        }

        public List<Order> GetOrdersByUserId(int userId) => _orderRepository.GetOrdersByUserId(userId);

        public List<Order> GetOrdersByRestaurantId(int restaurantId) => _orderRepository.GetOrdersByRestaurantId(restaurantId);

        public List<Order> GetOrdersByDateRange(DateTime start, DateTime end) => _orderRepository.GetOrdersByDateRange(start, end);

        public Order PlaceOrder(int userId)
        {
            var cartItems = _cartRepository.GetCartItemsByUserId(userId);
            if (!cartItems.Any())
                throw new CartEmptyException($"Cannot place order. Cart is empty for user ID {userId}");

            var restaurant = cartItems.First().MenuItem.Category.Restaurant;
            var subtotal = cartItems.Sum(item => item.Quantity * item.MenuItem.ItemPrice);
            var deliveryCharge = restaurant.DeliveryCharges;
            var gst = subtotal * 0.18m;
            var totalAmount = subtotal + deliveryCharge + gst;

            var order = new Order
            {
                UserId = userId,
                RestaurantId = restaurant.RestaurantId,
                TotalAmount = totalAmount,
                OrderDate = DateTime.Now,
                CartItems = new List<CartItem>()
            };

            foreach (var item in cartItems)
            {
                item.Order = order;
                order.CartItems.Add(item);
            }

            var createdOrder = _orderRepository.CreateOrder(order);
            _cartRepository.ClearCart(userId);
            return createdOrder;
        }

        public Order PlaceOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var cartItems = _cartRepository.GetCartItemsByUserId(order.UserId);
            if (!cartItems.Any())
                throw new CartEmptyException($"Cannot place order. Cart is empty for user ID {order.UserId}");

            order.CartItems = cartItems;
            var createdOrder = _orderRepository.CreateOrder(order);
            _cartRepository.ClearCart(order.UserId);
            return createdOrder;
        }

        public void UpdateOrder(Order order)
        {
            if (!_orderRepository.OrderExists(order.OrderId))
                throw new OrderNotFoundException($"Cannot update. Order with ID {order.OrderId} not found.");
            _orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                throw new OrderNotFoundException($"Cannot delete. Order with ID {orderId} not found.");
            _orderRepository.DeleteOrder(orderId);
        }

        public bool OrderExists(int orderId) => _orderRepository.OrderExists(orderId);
    }
}
