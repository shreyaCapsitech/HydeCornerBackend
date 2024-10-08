﻿using HydeBack.Models;
using HydeBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        // GET: api/<LoginController>
        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<Login>> GetLogins()
        {
            var logins = await _loginService.GetLogins();
            return Ok(logins);
        }

        // GET api/<LoginController>/5
        [HttpGet("{id:length(24)}")]
        [Authorize]
        public async Task<ActionResult<Login>> GetLoginById(string id)
        {
            var login = await _loginService.GetLoginById(id);
            if (login == null)
            {
                return NotFound();
            }
            return Ok(login);
        }

        // POST api/<LoginController>
        [HttpPost()]
        public async Task<IActionResult> AddLogins([FromBody] Login login)
        {
            if (login == null)
            {
                return BadRequest();
            }
            await _loginService.AddLogins(login);
            return CreatedAtAction(nameof(GetLoginById), new { id = login.Id }, login);
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> EditLogins(string id, [FromBody] Login login)
        {
            if (login == null)
            {
                return NotFound();
            }
            await _loginService.EditLogins(id, login);
            var updatedLogin = await _loginService.GetLoginById(id);
            if (updatedLogin == null)
            {
                return NotFound();
            }
            return Ok(updatedLogin);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Login login)
        {
            var authenticatedUser = await _loginService.AuthenticateUser(login.Username, login.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(new { username = authenticatedUser.Username, role = authenticatedUser.Role}); // Optionally, return a JWT token here for session management
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteLogins(string id)
        {
            var login = await _loginService.GetLoginById(id);
            if (login == null)
            {
                return NotFound();
            }
            await _loginService.DeleteLogins(id);
            return NoContent();
        }

    }
}
