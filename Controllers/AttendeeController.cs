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
    }
}