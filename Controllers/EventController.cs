using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ITB2203Application.Model;

namespace ITB2203Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly List<Event> _events = new List<Event>
        {
            new Event { Id = 1, SpeakerId = 1, Name = "Introduction to AI", Date = new DateTime(2024, 3, 15), Location = "Virtual" },
            new Event { Id = 2, SpeakerId = 2, Name = "Web Development Trends", Date = new DateTime(2024, 4, 10), Location = "Online" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents()
        {
            return _events;
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEventById(int id)
        {
            var ev = _events.FirstOrDefault(e => e.Id == id);
            if (ev == null)
            {
                return NotFound();
            }
            return ev;
        }
    }
}
