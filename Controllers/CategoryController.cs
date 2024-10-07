using HydeBack.Models;
using HydeBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<Category>> GetCategory()
        {
            var categorys = await _categoryService.GetCategory();
            return Ok(categorys);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id:length(24)}")]
        [Authorize]
        public async Task<ActionResult<Category>> GetCategoryById(string id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        // POST api/<CategoryController>
        [HttpPost()]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            await _categoryService.AddCategory(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> EditCategory(string id, [FromBody] Category category)
        {
            if (category == null)
            {
                return NotFound();
            }
            await _categoryService.EditCategory(id, category);
            var updatedCategory = await _categoryService.GetCategoryById(id);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
