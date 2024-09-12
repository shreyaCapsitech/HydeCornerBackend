using HydeBack.Models;
using HydeBack.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HydeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: api/<AdminController>
        [HttpGet()]
        public async Task<ActionResult<Admin>> GetAdmins()
        {
            var admins = await _adminService.GetAdmins();
            return Ok(admins);
        }

        // GET api/<AdminController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Admin>> GetAdminById(string id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        // POST api/<AdminController>
        [HttpPost()]
        public async Task<IActionResult> AddAdmins([FromBody] Admin admin)
        {
            if (admin == null)
            {
                return BadRequest();
            }
            await _adminService.AddAdmins(admin);
            return CreatedAtAction(nameof(GetAdminById), new { id = admin.Id }, admin);
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> EditAdmins(string id, [FromBody] Admin admin)
        {
            if (admin == null)
            {
                return NotFound();
            }
            await _adminService.EditAdmins(id, admin);
            var updatedAdmin = await _adminService.GetAdminById(id);
            if (updatedAdmin == null)
            {
                return NotFound();
            }
            return Ok(updatedAdmin);
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAdmins(string id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }
            await _adminService.DeleteAdmins(id);
            return NoContent();
        }
    }
}