using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineFoodDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] LocationDto dto)
        {
            _service.CreateLocation(dto);
            return Ok("Location created");
        }

        [HttpGet("All Locations")]
        public IActionResult GetAll()
        {
            var locations = _service.GetAllLocations();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var location = _service.GetLocationById(id);
            return location != null ? Ok(location) : NotFound("Location not found");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LocationDto dto)
        {
            var success = _service.UpdateLocation(id, dto);
            return success ? Ok("Location updated") : NotFound("Location not found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.DeleteLocation(id);
            return success ? Ok("Location deleted") : NotFound("Location not found");
        }
    }

}
