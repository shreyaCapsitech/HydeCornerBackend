using HydeBack.Models;
using HydeBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HydeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;
        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/<ItemController>
        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<Item>> GetItems()
        {
            var items = await _itemService.GetItems();
            return Ok(items);
        }

        // GET api/<ItemController>/5
        [HttpGet("{id:length(24)}")]
        [Authorize]
        public async Task<ActionResult<Item>> GetItemById(string id)
        {
            var item = await _itemService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }


        // POST api/<ItemController>
        [HttpPost()]
        public async Task<IActionResult> AddItems([FromBody] Item item)
        {
            if (item == null || string.IsNullOrEmpty(item.CategoryId) || string.IsNullOrEmpty(item.SubCategoryId))
            {
                return BadRequest("Invalid subcategory or missing CategoryId");
            }

            await _itemService.AddItems(item);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> EditItems(string id, [FromBody] Item item)
        {
            if (item == null)
            {
                return NotFound();
            }
            await _itemService.EditItems(id, item);
            var updatedItem = await _itemService.GetItemById(id);
            if (updatedItem == null)
            {
                return NotFound();
            }
            return Ok(updatedItem);
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteItems(string id)
        {
            var item = await _itemService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            await _itemService.DeleteItems(id);
            return NoContent();
        }
    }
}