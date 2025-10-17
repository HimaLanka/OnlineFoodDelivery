using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Exceptions;
using OnlineFoodDelivery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFoodDelivery.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("payment-process")]
        public IActionResult ProcessPayment(int orderId, string method, string? cardNumber = null, string? upiId = null)
        {
            try
            {
                var response = _paymentService.ProcessPayment(orderId, method, cardNumber, upiId);
                return Ok(response);
            }
            catch (OrderNotExistsException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (PaymentProcessingException ex)
            {
                return BadRequest(new {ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPut("update-payment-method")]
        public IActionResult UpdatePaymentMethod(int paymentId, string newMethod, string? cardNumber = null, string? upiId = null)
        {
            try
            {
                var response = _paymentService.UpdatePaymentMethod(paymentId, newMethod, cardNumber, upiId);
                return Ok(response);
            }
            catch (OrderNotExistsException ex)
            {
                return NotFound(new {ex.Message });
            }
            catch (PaymentProcessingException ex)
            {
                return BadRequest(new {ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while updating payment method." });
            }
        }

        [HttpGet("all-payments")]
        public IActionResult GetAllPayments()
        {
            try
            {
                var payments = _paymentService.GetAllPayments();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve payments." });
            }
        }

        [HttpGet("totalamount-month")]
        public IActionResult GetTotalAmountByMonth(int year, int month)
        {
            try
            {
                var total = _paymentService.GetTotalAmountByMonth(year, month);
                return Ok(total);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to calculate total amount." });
            }
        }
        [HttpDelete("{paymentId}")]
        public IActionResult DeletePayment(int paymentId)
        {
            try
            {
                var result = _paymentService.DeletePayment(paymentId);
                return Ok(new { message = result });
            }
            catch (PaymentProcessingException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (OrderNotExistsException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while deleting payment." });
            }
        }
    }
}
