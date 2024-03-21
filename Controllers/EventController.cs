using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly DataContext _context;

    public EventsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Event>> GetEvents(string? name = null)
    {
        var query = _context.Events!.AsQueryable();

        if (name != null)
            query = query.Where(x => x.Name != null && x.Name.ToUpper().Contains(name.ToUpper()));

        return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<TextReader> GetEvent(int id)
    {
        var Event = _context.Events!.FirstOrDefault(x => x.Id == id);

        if (Event == null)
        {
            return NotFound();
        }

        return Ok(Event);
    }

    [HttpPut("{id}")]
    public IActionResult PutEvent(int id, Event Event)
    {
        var dbEvent = _context.Events!.AsNoTracking().FirstOrDefault(x => x.Id == Event.Id);
        if (id != Event.Id || dbEvent == null)
        {
            return NotFound();
        }

        _context.Update(Event);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    public ActionResult<Event> PostEvent(Event Event)
    {
        var dbExercise = _context.Events!.Find(Event.Id);
        if (dbExercise == null)
        {
            _context.Add(Event);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEvent), new { Id = Event.Id }, Event);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(int id)
    {
        var Event = _context.Events!.Find(id);
        if (Event == null)
        {
            return NotFound();
        }

        _context.Remove(Event);
        _context.SaveChanges();

        return NoContent();
    }
}