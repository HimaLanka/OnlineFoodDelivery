using System.Collections.Generic;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Services
{
    public interface ICartService
    {
        List<CartItem> GetCartItemsByUserId(int userId);
        CartItem GetCartItemById(long cartItemId);
        void AddCartItem(CartItem cartItem);
        void UpdateCartItem(CartItem cartItem);
        void DeleteCartItem(long cartItemId);
        decimal GetCartTotal(int userId);
        Restaurant GetRestaurantForCart(int userId);
        void ClearCart(int userId);
      
    }
}
