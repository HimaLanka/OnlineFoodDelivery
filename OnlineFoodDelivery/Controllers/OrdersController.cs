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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orderService.GetOrderById(id);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
                return BadRequest("Order ID mismatch.");

            _orderService.UpdateOrder(order);
            return Ok("Order updated.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
            return Ok("Order deleted.");
        }

        [HttpPost("place/{userId}")]
        public IActionResult PlaceOrderFromCart(int userId)
        {
            var createdOrder = _orderService.PlaceOrder(userId);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderId }, createdOrder);
        }
    }
}
