using HydeBack.Models;
using HydeBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace HydeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserProfileService _userProfileService;
        public UserProfileController(UserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        // GET: api/<UserProfileController>
        [HttpGet()]
        public async Task<ActionResult<UserProfile>> GetUserProfile()
        {
            var userProfiles = await _userProfileService.GetUserProfile();
            return Ok(userProfiles);
        }

        // GET api/<UserProfileController>/5
        [HttpGet("{id:length(24)}")]
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
