using HydeBack.Models;
using HydeBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace HydeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategoryService _subCategoryService;
        public SubCategoryController(SubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet()]
        public async Task<ActionResult<SubCategory>> GetCategory()
        {
            var subCategorys = await _subCategoryService.GetSubCategory();
            return Ok(subCategorys);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<SubCategory>> GetSubCategoryById(string id)
        {
            var subCategory = await _subCategoryService.GetSubCategoryById(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return Ok(subCategory);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> AddSubCategory([FromBody] SubCategory subCategory)
        {
            if (subCategory == null || string.IsNullOrEmpty(subCategory.CategoryId))
            {
                return BadRequest("Invalid subcategory or missing CategoryId");
            }

            await _subCategoryService.AddSubCategory(subCategory);
            return CreatedAtAction(nameof(GetSubCategoryById), new { id = subCategory.Id }, subCategory);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> EditSubCategory(string id, [FromBody] SubCategory subCategory)
        {
            if (subCategory == null)
            {
                return NotFound();
            }
            await _subCategoryService.EditSubCategory(id, subCategory);
            var updatedCategory = await _subCategoryService.GetSubCategoryById(id);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteSubCategory(string id)
        {
            var subCategory = await _subCategoryService.GetSubCategoryById(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            await _subCategoryService.DeleteSubCategory(id);
            return NoContent();
        }
    }
}
