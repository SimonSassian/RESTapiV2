using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ITB2203Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly List<Speaker> _speakers = new List<Speaker>
        {
            new Speaker { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
            new Speaker { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Speaker>> GetSpeakers()
        {
            return _speakers;
        }

        [HttpGet("{id}")]
        public ActionResult<Speaker> GetSpeakerById(int id)
        {
            var speaker = _speakers.FirstOrDefault(s => s.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }
            return speaker;
        }
    }

    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
