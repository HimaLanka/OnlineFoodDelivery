using System.Collections.Generic;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public interface ICartRepository
    {
        List<CartItem> GetCartItemsByUserId(int userId);
        CartItem GetCartItemById(long cartItemId);
        void AddCartItem(CartItem cartItem);
        void UpdateCartItem(CartItem cartItem);
        void DeleteCartItem(long cartItemId);
        decimal GetCartTotal(int userId);
        void ClearCart(int userId);

        // ✅ New method to support restaurant mismatch validation
        MenuItem GetMenuItemById(long menuItemId);
    }
}

