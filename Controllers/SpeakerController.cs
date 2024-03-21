using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITB2203Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpeakersController : ControllerBase
{
    private readonly DataContext _context;

    public SpeakersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Speaker>> GetSpeaker(string? name = null)
    {
        var query = _context.Speakers!.AsQueryable();

        if (name != null)
            query = query.Where(x => x.Name != null && x.Name.ToUpper().Contains(name.ToUpper()));

        return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<TextReader> GetSpeaker(int id)
    {
        var Speaker = _context.Speakers!.FirstOrDefault(x => x.Id == id);

        if (Speaker == null)
        {
            return NotFound();
        }

        return Ok(Speaker);
    }

    [HttpPut("{id}")]
    public IActionResult PutSpeaker(int id, Speaker Speaker)
    {
        var dbSpeaker = _context.Speakers!.AsNoTracking().FirstOrDefault(x => x.Id == Speaker.Id);
        if (id != Speaker.Id || dbSpeaker == null)
        {
            return NotFound();
        }

        _context.Update(Speaker);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    public ActionResult<Speaker> PostSpeaker(Speaker Speaker)
    {
        var dbExercise = _context.Speakers!.Find(Speaker.Id);
        if (dbExercise == null)
        {
            _context.Add(Speaker);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSpeaker), new { Id = Speaker.Id }, Speaker);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSpeaker(int id)
    {
        var Speaker = _context.Speakers!.Find(id);
        if (Speaker == null)
        {
            return NotFound();
        }

        _context.Remove(Speaker);
        _context.SaveChanges();

        return NoContent();
    }
}
