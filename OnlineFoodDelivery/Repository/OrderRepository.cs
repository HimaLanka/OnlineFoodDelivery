using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnlineFoodDeliveryContext _context;

        public OrderRepository(OnlineFoodDeliveryContext context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Order
                .Include(o => o.CartItems)
                    .ThenInclude(ci => ci.MenuItem)
                        .ThenInclude(m => m.Category)
                            .ThenInclude(c => c.Restaurant)
                .Include(o => o.User)
                .Include(o => o.Restaurant)
                .ToList();
        }


        public Order GetOrderById(int orderId)
        {
            return _context.Order
                .Include(o => o.User)
                .Include(o => o.Restaurant)
                .FirstOrDefault(o => o.OrderId == orderId);
        }



        public List<Order> GetOrdersByUserId(int userId)
        {
            return _context.Order
                .Where(o => o.UserId == userId)
                .Include(o => o.Restaurant)
                .ToList();
        }

        public List<Order> GetOrdersByRestaurantId(int restaurantId)
        {
            return _context.Order
                .Where(o => o.RestaurantId == restaurantId)
                .Include(o => o.User)
                .ToList();
        }

        public List<Order> GetOrdersByDateRange(DateTime start, DateTime end)
        {
            return _context.Order
                .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                .Include(o => o.User)
                .Include(o => o.Restaurant)
                .ToList();
        }

        public Order CreateOrder(Order order)
        {
            // ✅ Make sure each cart item is tracked and updated
            foreach (var cartItem in order.CartItems)
            {
                _context.CartItem.Update(cartItem); // This saves the OrderId link
            }

            _context.Order.Add(order); // Save the order itself
            _context.SaveChanges();    // Commit everything to the database
            return order;
        }


        public void UpdateOrder(Order order)
        {
            _context.Order.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Order.Find(orderId);
            if (order != null)
            {
                _context.Order.Remove(order);
                _context.SaveChanges();
            }
        }

        public bool OrderExists(int orderId)
        {
            return _context.Order.Any(o => o.OrderId == orderId);
        }
    }
}

