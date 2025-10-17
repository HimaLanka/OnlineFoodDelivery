using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _service;

        public MenuItemController(IMenuItemService service)
        {
            _service = service;
        }

        [Authorize(Roles = "restaurantowner")]
        [HttpPost]
        public IActionResult AddItem([FromBody] MenuItemDto dto)
        {

            var success = _service.AddItem1(dto);
            return success ? Ok("Item Added") : BadRequest("Something Went wrong.");
        }

        [Authorize(Roles = "restaurantowner")]
        [HttpPut("{Iid}")]
        public IActionResult UpdateItem(long Iid, [FromBody] MenuItemDto dto)
        {

            var success = _service.UpdateItem1(Iid, dto);
            return success ? Ok("Item updated") : NotFound("Item not found or unauthorized");
        }

        [Authorize(Roles = "restaurantowner")]
        [HttpDelete("{Iid}")]
        public IActionResult DeleteItem(long Iid)
        {

            var success = _service.RemoveItem1(Iid);
            return success ? Ok("Item deleted") : NotFound("Item not found or unauthorized");
        }

        [Authorize(Roles = "restaurantowner")]
        [HttpGet("GetItemByIid")]
        public IActionResult GetItemById(long Iid)
        {

            var item = _service.GetItemById1(Iid);
            return item != null ? Ok(item) : NotFound("Item not found");
        }

        [AllowAnonymous]
        [HttpGet("GetItemByName")]
        public IActionResult GetItemByName(string name)
        {
            
            var item = _service.GetItemByName1(name);
            return item != null ? Ok(item) : NotFound("Item not found");
        }

        [AllowAnonymous]
        [HttpGet("GetAllItems")]
        public IActionResult GetAllItems()
        {
            var success = _service.GetAllItems1();
            return Ok(success);
        }

        [AllowAnonymous]
        [HttpGet("GetItemsByCid")]
        public IActionResult GetItemsByCid(long Cid)
        {
            var success = _service.GetItemsByCategoryId1(Cid);
            return Ok(success);
        }





    }
}
