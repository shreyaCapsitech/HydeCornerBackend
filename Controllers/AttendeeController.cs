using HydeBack.Models;
using HydeBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HydeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        private readonly AttendeeService _attendeeService;
        public AttendeeController(AttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }

        // GET: api/<AttendeeController>
        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<Attendee>> GetAttendee()
        {
            var attendees = await _attendeeService.GetAttendee();
            return Ok(attendees);
        }

        // GET api/<AttendeeController>/5
        [HttpGet("{id:length(24)}")]
        [Authorize]
        public async Task<ActionResult<Attendee>> GetAttendeeById(string id)
        {
            var attendee = await _attendeeService.GetAttendeeById(id);
            if (attendee == null)
            {
                return NotFound();
            }
            return Ok(attendee);
        }


        // POST api/<AttendeeController>
        [HttpPost()]
        public async Task<IActionResult> AddAttendee([FromBody] Attendee attendee)
        {
            if (attendee == null)
            {
                return BadRequest();
            }
            await _attendeeService.AddAttendee(attendee);
            return CreatedAtAction(nameof(GetAttendeeById), new { id = attendee.Id }, attendee);
        }

        // PUT api/<AttendeeController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> EditAttendee(string id, [FromBody] Attendee attendee)
        {
            if (attendee == null)
            {
                return NotFound();
            }
            await _attendeeService.EditAttendee(id, attendee);
            var updatedAttendee = await _attendeeService.GetAttendeeById(id);
            if (updatedAttendee == null)
            {
                return NotFound();
            }
            return Ok(updatedAttendee);
        }

        // DELETE api/<AttendeeController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAttendee(string id)
        {
            var attendee = await _attendeeService.GetAttendeeById(id);
            if (attendee == null)
            {
                return NotFound();
            }
            await _attendeeService.DeleteAttendee(id);
            return NoContent();
        }
    }
}
