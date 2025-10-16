using System;
using Microsoft.AspNetCore.Mvc;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Services;
using OnlineFoodDelivery.Exceptions;
using OnlineFoodDelivery.Aspect;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandler]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartItemsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetCartItemsByUserId(int userId)
        {
            var items = _cartService.GetCartItemsByUserId(userId);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetCartItem(long id)
        {
            var item = _cartService.GetCartItemById(id);
            if (item == null)
                throw new CartItemNotFoundException($"Cart item with ID {id} not found.");
            return Ok(item);
        }

        [HttpPost]
        public IActionResult PostCartItem(CartItemDto dto)
        {
            try
            {
                var cartItem = new CartItem
                {
                    UserId = dto.UserId,
                    MenuItemId = (int)dto.MenuItemId,
                    Quantity = dto.Quantity,
                    AddedAt = DateTime.Now
                };

                _cartService.AddCartItem(cartItem);
                return Ok("Item added to cart.");
            }
            catch (RestaurantMismatchException ex)
            {
                return Content(ex.Message, "text/plain");
            }
            catch (CartItemNotFoundException ex)
            {
                return Content(ex.Message, "text/plain", System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutCartItem(long id, CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
                return BadRequest("Cart item ID mismatch.");

            _cartService.UpdateCartItem(cartItem);
            return Ok("Cart item updated.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCartItem(long id)
        {
            var item = _cartService.GetCartItemById(id);
            if (item == null)
                throw new CartItemNotFoundException($"Cart item with ID {id} not found.");

            _cartService.DeleteCartItem(id);
            return Ok("Cart item deleted.");
        }

        [HttpGet("total/{userId}")]
        public IActionResult GetCartTotal(int userId)
        {
            var total = _cartService.GetCartTotal(userId);
            return Ok(new { totalAmount = total });
        }

        [HttpGet("restaurant/{userId}")]
        public IActionResult GetRestaurantForCart(int userId)
        {
            var restaurant = _cartService.GetRestaurantForCart(userId);
            return Ok(restaurant);
        }

        [HttpDelete("clear/{userId}")]
        public IActionResult ClearCart(int userId)
        {
            _cartService.ClearCart(userId);
            return Ok("Cart cleared.");
        }
    }
}
