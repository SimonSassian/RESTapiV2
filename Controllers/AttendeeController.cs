using ITB2203Application;
using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class AttendeesController : ControllerBase
{
    private readonly DataContext _context;

    public AttendeesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Attendee>> GetAttendees(string? name = null)
    {
        var query = _context.Attendees!.AsQueryable();

        if (name != null)
            query = query.Where(x => x.Name != null && x.Name.ToUpper().Contains(name.ToUpper()));

        return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<TextReader> GetAttendee(int id)
    {
        var Attendee = _context.Attendees!.FirstOrDefault(x => x.Id == id);

        if (Attendee == null)
        {
            return NotFound();
        }

        return Ok(Attendee);
    }

    [HttpPut("{id}")]
    public IActionResult PutAttendee(int id, Attendee Attendee)
    {
        var dbAttendee = _context.Attendees!.AsNoTracking().FirstOrDefault(x => x.Id == Attendee.Id);
        if (id != Attendee.Id || dbAttendee == null)
        {
            return NotFound();
        }

        _context.Update(Attendee);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    public ActionResult<Attendee> PostAttendee(Attendee Attendee)
    {
        var dbExercise = _context.Attendees!.Find(Attendee.Id);
        if (dbExercise == null)
        {
            _context.Add(Attendee);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAttendee), new { Id = Attendee.Id }, Attendee);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAttendee(int id)
    {
        var Attendee = _context.Attendees!.Find(id);
        if (Attendee == null)
        {
            return NotFound();
        }

        _context.Remove(Attendee);
        _context.SaveChanges();

        return NoContent();
    }
}