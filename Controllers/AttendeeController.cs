using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ITB2203Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        private readonly List<Attendee> _attendees = new List<Attendee>
        {
            new Attendee { Id = 1, EventId = 1, Name = "Alice", Email = "alice@example.com", RegistrationTime = DateTime.UtcNow },
            new Attendee { Id = 2, EventId = 2, Name = "Bob", Email = "bob@example.com", RegistrationTime = DateTime.UtcNow }
            // Add more attendees as needed
        };

        [HttpGet]
        public ActionResult<IEnumerable<Attendee>> GetAttendees()
        {
            return _attendees;
        }

        [HttpGet("{id}")]
        public ActionResult<Attendee> GetAttendeeById(int id)
        {
            var attendee = _attendees.FirstOrDefault(a => a.Id == id);
            if (attendee == null)
            {
                return NotFound();
            }
            return attendee;
        }

        [HttpPost]
        public ActionResult<Attendee> CreateAttendee(Attendee attendee)
        {
            // Simulate auto-increment ID
            attendee.Id = _attendees.Count + 1;
            _attendees.Add(attendee);
            return CreatedAtAction(nameof(GetAttendeeById), new { id = attendee.Id }, attendee);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAttendee(int id, Attendee updatedAttendee)
        {
            var attendee = _attendees.FirstOrDefault(a => a.Id == id);
            if (attendee == null)
            {
                return NotFound();
            }

            attendee.EventId = updatedAttendee.EventId;
            attendee.Name = updatedAttendee.Name;
            attendee.Email = updatedAttendee.Email;
            attendee.RegistrationTime = updatedAttendee.RegistrationTime;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttendee(int id)
        {
            var attendee = _attendees.FirstOrDefault(a => a.Id == id);
            if (attendee == null)
            {
                return NotFound();
            }

            _attendees.Remove(attendee);
            return NoContent();
        }
    }
}
