using HydeBack.Models;
using HydeBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HydeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserProfileController : ControllerBase
    {
        private readonly UserProfileService _userProfileService;
        private readonly IConfiguration _configuration;

        public UserProfileController(UserProfileService userProfileService, IConfiguration configuration)
        {
            _userProfileService = userProfileService;
            _configuration = configuration;
        }

        // GET: api/<UserProfileController>
        /*[HttpGet()]
        public async Task<ActionResult<UserProfile>> GetUserProfile()
        {
            var userProfiles = await _userProfileService.GetUserProfile();
            return Ok(userProfiles);
        }*/

        // GET api/<UserProfileController>/5
        /*  [HttpGet("{id:length(24)}")]
          public async Task<ActionResult<UserProfile>> GetUserProfileById(string id)
          {
              var userProfile = await _userProfileService.GetUserProfileById(id);
              if (userProfile == null)
              {
                  return NotFound();
              }
              return Ok(userProfile);
          }*/

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<List<UserProfile>>> GetUserProfile()
        {
            var userProfiles = await _userProfileService.GetUserProfile();
            return Ok(userProfiles);
        }
       
        [HttpGet("{id:length(24)}")]
        [Authorize]
        public async Task<ActionResult<UserProfile>> GetUserProfileById(string id)
        {
            var userProfile = await _userProfileService.GetUserProfileById(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }


        // POST api/<UserProfileController>
        [HttpPost()]
        public async Task<IActionResult> AddUserProfile([FromBody] UserProfile userProfile)
        {
            if (userProfile == null)
            {
                return BadRequest();
            }
            await _userProfileService.AddUserProfile(userProfile);
            return CreatedAtAction(nameof(GetUserProfileById), new { id = userProfile.Id }, userProfile);
        }

        // PUT api/<UserProfileController>/5
        [HttpPut("{id:length(24)}")]
      
        public async Task<IActionResult> EditUserProfile(string id, [FromBody] UserProfile userProfile)
        {
            if (userProfile == null)
            {
                return NotFound();
            }
            await _userProfileService.EditUserProfile(id, userProfile);
            var updatedUserProfile = await _userProfileService.GetUserProfileById(id);
            if (updatedUserProfile == null)
            {
                return NotFound();
            }
            return Ok(updatedUserProfile);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Login login)
        {
            var authenticatedUser = await _userProfileService.AuthenticateUser(login.Username, login.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);  // Use _configuration if injected
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, authenticatedUser.Username),
            new Claim(ClaimTypes.Role, authenticatedUser.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(24), // Token expires in 24 hour
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                Username = authenticatedUser.Username,
                Role = authenticatedUser.Role,
                Name = authenticatedUser.Name
            });
        }

        //return Ok(new { username = authenticatedUser.Username, role = authenticatedUser.Role, name = authenticatedUser.Name }); // Optionally, return a JWT token here for session management

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            var result = await _userProfileService.ChangePassword(model.Username, model.OldPassword, model.NewPassword);
            if (!result)
            {
                return BadRequest(new { message = "Failed to change password. Old password may be incorrect." });
            }

            return Ok(new { message = "Password successfully changed" });
        }

        // DELETE api/<UserProfileController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var userProfile = await _userProfileService.GetUserProfileById(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            await _userProfileService.DeleteUserProfile(id);
            return NoContent();
        }
    }
}
