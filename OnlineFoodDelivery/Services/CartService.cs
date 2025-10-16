using System;
using System.Collections.Generic;
using System.Linq;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;
using OnlineFoodDelivery.Exceptions;

namespace OnlineFoodDelivery.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public List<CartItem> GetCartItemsByUserId(int userId)
        {
            return _cartRepository.GetCartItemsByUserId(userId);
        }

        public CartItem GetCartItemById(long cartItemId)
        {
            return _cartRepository.GetCartItemById(cartItemId);
        }

        public void AddCartItem(CartItem cartItem)
        {
            // Load full MenuItem from DB
            var newMenuItem = _cartRepository.GetMenuItemById(cartItem.MenuItemId);
            if (newMenuItem == null)
                throw new CartItemNotFoundException($"Menu item with ID {cartItem.MenuItemId} not found.");

            cartItem.MenuItem = newMenuItem;

            var existingItems = _cartRepository.GetCartItemsByUserId(cartItem.UserId);
            if (existingItems.Any())
            {
                var existingRestaurantId = existingItems
                    .Select(item => item.MenuItem.Category.RestaurantId)
                    .Distinct()
                    .Single();

                var newItemRestaurantId = newMenuItem.Category.RestaurantId;

                if (existingRestaurantId != newItemRestaurantId)
                    throw new RestaurantMismatchException("You can only add items from one restaurant to your cart.");
            }

            _cartRepository.AddCartItem(cartItem);
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _cartRepository.UpdateCartItem(cartItem);
        }

        public void DeleteCartItem(long cartItemId)
        {
            _cartRepository.DeleteCartItem(cartItemId);
        }

        public decimal GetCartTotal(int userId)
        {
            var cartItems = _cartRepository.GetCartItemsByUserId(userId);
            if (cartItems == null || !cartItems.Any())
                throw new CartEmptyException($"Cart is empty for user ID {userId}");

            var subtotal = cartItems.Sum(item => item.Quantity * item.MenuItem.ItemPrice);
            var restaurant = cartItems.First().MenuItem.Category.Restaurant;
            var deliveryCharge = restaurant.DeliveryCharges;
            var gst = subtotal * 0.18m;
            return subtotal + deliveryCharge + gst;
        }

        public Restaurant? GetRestaurantForCart(int userId)
        {
            var cartItems = _cartRepository.GetCartItemsByUserId(userId);
            return cartItems.FirstOrDefault()?.MenuItem.Category.Restaurant;
        }

        public void ClearCart(int userId)
        {
            _cartRepository.ClearCart(userId);
        }
    }
}
