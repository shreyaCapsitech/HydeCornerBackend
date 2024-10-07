using HydeBack.Models;
using HydeBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<Order>> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id:length(24)}")]
        [Authorize]
        public async Task<ActionResult<Order>> GetOrderById(string id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost()]
        [AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> AddOrders([FromBody] Order order)
        {
            if (order == null || order.UserId == null || !order.UserId.Any() || order.ItemId == null || !order.ItemId.Any() || order.Quantities == null || !order.Quantities.Any())
            {
                return BadRequest("Invalid order or missing item/quantity data");
            }

            await _orderService.AddOrders(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> EditOrders(string id, [FromBody] Order order)
        {
            if (order == null)
            {
                return NotFound();
            }
            await _orderService.EditOrders(id, order);
            var updatedOrder = await _orderService.GetOrderById(id);
            if (updatedOrder == null)
            {
                return NotFound();
            }
            return Ok(updatedOrder);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteOrders(string id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            await _orderService.DeleteOrders(id);
            return NoContent();
        }
    }
}
