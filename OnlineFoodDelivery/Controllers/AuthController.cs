using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Exceptions;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;
using OnlineFoodDelivery.Services;


namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]//attribute routing
    [ApiController]
    public class AuthController : ControllerBase

    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)

        {

            _authService = authService;

        }

        [HttpPost("register")]//controller action
        public IActionResult Register([FromBody] User us)
        {
            // Basic null check
            if (us == null)
                return BadRequest("User data is required");

            // Role validation using if-else
            string role = us.Role?.Trim().ToLower();
            if (role != "customer" && role != "deliveragent" && role != "restaurantowner" && role != "admin")

                return BadRequest("Invalid role. Allowed roles: Customer, DeliverAgent, RestaurantOwner");

            // Call service
            var result = _authService.Register(us);

            if (result.Contains("already"))
                return Conflict(result);

            return Ok(result);
        }


        [HttpPost("login")]

      
        public IActionResult Login([FromBody] OnlineFoodDelivery.Model.LoginRequest usLog)
        {
            var result = _authService.Login(usLog);

            if (result == "Invalid email or phone" || result == "Invalid password" || result == "Invalid role")
                return Unauthorized(result); // returns the specific error

            return Ok(new { Token = result }); // result is the token
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_authService.GetAllUsers());
        }

        [Authorize(Roles = "customer,deliveragent,restaurantowner")]

        [HttpGet("Id")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _authService.GetUserById(id);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Roles = "customer,deliveragent,restaurantowner")]

        [HttpGet("email")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var user = _authService.GetUserByEmail(email);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "customer,deliveragent,restaurantowner")]

        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            return Ok(_authService.DeleteUserById(id));
        }
        [Authorize(Roles = "Customer,DeliverAgent,RestaurantOwner")]
        [HttpDelete("email")]
        public IActionResult DeleteUserByEmail([FromBody] User user)
        {
            try
            {
                var result = _authService.DeleteUserByEmail(user.Email);
                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize(Roles = "customer,deliveragent,restaurantowner")]
        [HttpPut("by-id/{id}")]
        public IActionResult UpdateUserDetails(int id, User user)
        {
            return Ok(_authService.UpdateUserById(id, user));

        }
        [Authorize(Roles = "customer,deliveragent,restaurantowner")]
        [HttpPut("by-email/{email}")]
        public IActionResult UpdateUserDetailsByEmail(string email, User user)
        {
            var result = _authService.UpdateUserByEmail(email, user);

            if (result == 0)
                return NotFound($"User with email {email} does not exist.");

            return Ok("Details updated successfully");


        }
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ResetPasswordRequest request)
        {
            var result = _authService.ForgotPassword(request.Email, request.NewPassword);

            if (result == "User not found")
                return NotFound(result);

            return Ok(result);
        }






    }
}



