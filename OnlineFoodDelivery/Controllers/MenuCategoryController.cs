using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Exceptions;
//using OnlineFoodDelivery.Migrations;
using OnlineFoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCategoryController : ControllerBase
    {
        private readonly IMenuCategoryService _service;

        public MenuCategoryController(IMenuCategoryService service)
        {
            _service = service;
        }

        [Authorize(Roles = "restaurantowner")]
        [HttpPost]
        public IActionResult AddCategory([FromBody] MenuCategoryDto dto)
        {

            var success = _service.AddCategory1(dto);
            return success ? Ok("Category Added") : BadRequest("Something Went wrong.");
            //return StatusCode(201, _service.AddCategory1(dto));
        }


        [Authorize(Roles = "restaurantowner")]
        [HttpPut("{Cid}")]
        public IActionResult UpdateCategory(long Cid, [FromBody] MenuCategoryDto dto)
        {
            try
            {
                var success = _service.UpdateCategory1(Cid, dto);
                return Ok("Category updated");
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [Authorize(Roles = "restaurantowner")]
        [HttpDelete("{Cid}")]
        public IActionResult DeleteCategory(long Cid)
        {
            try
            {
                var success = _service.RemoveCategory1(Cid);
                return Ok("Category deleted");
            }
            catch(CategoryNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "restaurantowner")]
        [HttpGet("GetCategoryByCid")]
        public IActionResult GetcategoryById(long Cid)
        {
            
            var category = _service.GetCategoryById1(Cid);
            return category != null ? Ok(category) : NotFound("Category not found");
        }


        [AllowAnonymous]
        [HttpGet("GetAllCategoriesByRid")]
        public IActionResult GetAllCategories(long Rid)
        {
            var categories = _service.AllCategoriesByResId1(Rid);
            return Ok(categories);
        }



    }
} 
