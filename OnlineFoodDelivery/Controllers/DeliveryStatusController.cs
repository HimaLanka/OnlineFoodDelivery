using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineFoodDelivery.Services;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryStatusController : ControllerBase
    {

        private readonly IDeliveryStatusService _service;

        public DeliveryStatusController(IDeliveryStatusService service)
        {
            _service = service;
        }



        [HttpGet]
        public IActionResult GetAllDeliveries()
        {
            return Ok(_service.GetAllDeliveries());
        }



        [HttpGet("delivery/{deliveryId}")]
        public ActionResult<DeliveryStatus> GetDeliveryById(int deliveryId)
        {
            try
            {
                var delivery = _service.GetDeliveryById(deliveryId);
                return Ok(delivery);
            }
            catch (DeliveryByIDNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpPost("assign/{orderId}")]
        //public IActionResult AssignAgent(int orderId)
        //{
        //    try
        //    {
        //        var result = _service.AssignAgentToOrder(orderId);
        //        return Ok(result);
        //    }
        //    catch (AgentsNotAvailableException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("assign/{orderId}")]
        public IActionResult AssignAgent(int orderId)
        {
            try
            {
                var result = _service.AssignAgentToOrder(orderId);
                return Ok(result);
            }
            catch (OrderNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AgentsNotAvailableException ex)
            {
                return BadRequest(ex.Message);
            }
        }





        //[HttpPut("update/{deliveryId}")]
        //public async Task<IActionResult> UpdateStatus(int deliveryId, [FromBody] string status)
        //{
        //    try
        //    {
        //        var result = await _service.UpdateDeliveryStatus(deliveryId, status);
        //        return Ok(result);
        //    }
        //    catch (DeliveryByIDNotFoundException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpPut("update/{deliveryId}")]
        public async Task<IActionResult> UpdateStatus(int deliveryId, [FromBody] string status)
        {
            try
            {
                var result = await _service.UpdateDeliveryStatus(deliveryId, status);
                return Ok(result);
            }
            catch (DeliveryByIDNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CustomerNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EmailDeliveryException ex)
            {
                return BadRequest(ex.Message);
            }
        }






        [HttpGet("agent/{agentId}/monthly-count")]
        public IActionResult GetMonthlyCount(int agentId)
        {
            try
            {
                var result = _service.GetMonthlyDeliveryCountByAgent(agentId);
                return Ok(result);
            }
            catch (AgentsNotAvailableException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("agent/{agentId}/weekly-count")]
        public IActionResult GetWeeklyCount(int agentId)
        {
            try
            {
                var result = _service.GetWeeklyDeliveryCountByAgent(agentId);
                return Ok(result);
            }
            catch (AgentsNotAvailableException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


















        //[HttpGet("test-email")]
        //public async Task<IActionResult> SendTestEmail([FromServices] IEmailService emailService)
        //{
        //    await emailService.SendEmailAsync(
        //        "susmithakothapalli18@gmail.com", // use your own email here
        //        "Test Email from DeliveryApp",
        //        "This is a test email to verify SMTP setup."
        //    );

        //    return Ok("Test email sent.");
        //}

        //[HttpPut("update/{deliveryId}")]
        //public IActionResult UpdateStatus(int deliveryId, [FromBody] string status)
        //{
        //    try
        //    {
        //        var result = _service.UpdateDeliveryStatus(deliveryId, status);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //[HttpPut("update/{deliveryId}")]
        //public IActionResult UpdateStatus(int deliveryId, [FromBody] string status)
        //{
        //    try
        //    {
        //        var result = _service.UpdateDeliveryStatus(deliveryId, status);

        //        // Build notification message if status is Delivered and customer is available
        //        string message = null;
        //        if (status == "Delivered" && result.Order?.Customer?.Username != null)
        //        {
        //            message = $"Notification to {result.Order.Customer.Username}: Your order has been delivered.";
        //        }

        //        return Ok(new { result, message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("test-email")]
        //public async Task<IActionResult> TestEmail([FromServices] IEmailService emailService)
        //{
        //    await emailService.SendEmailAsync(
        //        "susmithakothapalli18@gmail.com",
        //        "Test Email",
        //        "This is a test email from DeliveryApp"
        //    );
        //    return Ok("Test email sent.");
        //}