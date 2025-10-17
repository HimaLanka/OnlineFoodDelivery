using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace OnlineFoodDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase

    {

        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)

        {

            _service = service;

        }

        // ✅ Create a restaurant (only if one doesn't exist for this owner)

        [Authorize(Roles = "restaurantowner")]

        [HttpPost]

        public IActionResult Create([FromBody] RestaurantDto dto)

        {

            //var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0");

            var success = _service.CreateRestaurant(dto);

            return success ? Ok("Restaurant created") : BadRequest("Restaurant already exists or invalid location");

        }

        // ✅ Get the restaurant owned by the logged-in user

        [Authorize(Roles = "restaurantowner")]

        [HttpGet("ResByUserId")]

        public IActionResult GetMyRestaurantsByOwnerId(int userId)

        {

            //var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            var restaurant = _service.GetRestaurantByOwnerId(userId);

            return restaurant != null ? Ok(restaurant) : NotFound("No restaurant found");

        }

        // ✅ Update restaurant (only if owned by user)

        [Authorize(Roles = "restaurantowner")]

        [HttpPut("{Rid}")]

        public IActionResult Update(long id, [FromBody] RestaurantDto dto)

        {

            //var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0");

            var userId = dto.Id;

            var success = _service.UpdateRestaurant(id, dto, userId);

            return success ? Ok("Restaurant updated") : NotFound("Restaurant not found or unauthorized");

        }

        // ✅ Delete restaurant (only if owned by user)

        [Authorize(Roles = "restaurantowner")]

        [HttpDelete("{id}")]

        public IActionResult Delete(long id, int userid)

        {

            //var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0");

            var userId = userid;

            var success = _service.DeleteRestaurant(id, userId);

            return success ? Ok("Restaurant deleted") : NotFound("Restaurant not found or unauthorized");

        }

        // ✅ Get all restaurants (public)

        [AllowAnonymous]

        [HttpGet("All Restaurants")]

        public IActionResult GetAllRestaurants()

        {

            var restaurants = _service.GetAll();

            return Ok(restaurants);

        }

        // ✅ Search restaurants by name (public)

        [AllowAnonymous]

        [HttpGet("searchByName")]

        public IActionResult GetByName([FromQuery] string name)

        {

            var restaurants = _service.GetByName(name);

            return restaurants != null ? Ok(restaurants) : NotFound("Restaurant not found");

        }


        // ✅ Filter restaurants by location (public)

        [HttpGet("location/id/{locationId}")]

        public IActionResult GetResByLocationId(int locationId)

        {

            var restaurants = _service.GetResByLocationId(locationId);

            return restaurants != null ? Ok(restaurants) : NotFound("Restaurant not found");

        }

        [AllowAnonymous]

        [HttpGet("location/state/{State}")]

        public IActionResult GetResByState(string State)

        {

            var restaurants = _service.GetResByState(State);

            return restaurants != null ? Ok(restaurants) : NotFound("Restaurant not found");

        }

        [AllowAnonymous]

        [HttpGet("location/city/{City}")]

        public IActionResult GetResByCity(string City)

        //public IActionResult GetResByCity(string locationCity), if we give like this

        //it will ask City(above as required) and locationCity(optional) also.

        {

            var restaurants = _service.GetResByCity(City);

            return restaurants != null ? Ok(restaurants) : NotFound("Restaurant not found");

        }

        [AllowAnonymous]

        [HttpGet("location/area/{Area}")]

        public IActionResult GetResByArea(string Area)

        {

            var restaurants = _service.GetResByArea(Area);

            return restaurants != null ? Ok(restaurants) : NotFound("Restaurant not found");

        }

        [AllowAnonymous]

        [HttpGet("location/pincode/{Pincode}")]

        public IActionResult GetResByPincode(string Pincode)

        {

            var restaurants = _service.GetResByPincode(Pincode);

            return restaurants != null ? Ok(restaurants) : NotFound("Restaurant not found");

        }



    }


}