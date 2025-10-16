using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly OnlineFoodDeliveryContext _context;

        public CartRepository(OnlineFoodDeliveryContext context)
        {
            _context = context;
        }

        public List<CartItem> GetCartItemsByUserId(int userId)
        {
            return _context.CartItem
                .Include(c => c.MenuItem)
                    .ThenInclude(m => m.Category)
                        .ThenInclude(c => c.Restaurant)
                .Where(c => c.UserId == userId)
                .ToList();
        }

        public CartItem GetCartItemById(long cartItemId)
        {
            return _context.CartItem
                .Include(c => c.MenuItem)
                    .ThenInclude(m => m.Category)
                        .ThenInclude(c => c.Restaurant)
                .FirstOrDefault(c => c.CartItemId == cartItemId);
        }

        public MenuItem GetMenuItemById(long menuItemId)
        {
            return _context.MenuItem
                .Include(m => m.Category)
                    .ThenInclude(c => c.Restaurant)
                .FirstOrDefault(m => m.MenuItemId == menuItemId);
        }

        public void AddCartItem(CartItem cartItem)
        {
            _context.CartItem.Add(cartItem);
            _context.SaveChanges();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _context.CartItem.Update(cartItem);
            _context.SaveChanges();
        }

        public void DeleteCartItem(long cartItemId)
        {
            var item = _context.CartItem.Find(cartItemId);
            if (item != null)
            {
                _context.CartItem.Remove(item);
                _context.SaveChanges();
            }
        }

        public decimal GetCartTotal(int userId)
        {
            var cartItems = GetCartItemsByUserId(userId);
            if (!cartItems.Any()) return 0;

            var subtotal = cartItems.Sum(item => item.Quantity * item.MenuItem.ItemPrice);
            var deliveryCharge = cartItems.First().MenuItem.Category.Restaurant.DeliveryCharges;
            var gst = subtotal * 0.18m;

            return subtotal + deliveryCharge + gst;
        }

        public void ClearCart(int userId)
        {
            var items = _context.CartItem.Where(c => c.UserId == userId).ToList();
            _context.CartItem.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}
