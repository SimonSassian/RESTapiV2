using Microsoft.AspNetCore.Mvc;

namespace ITB2203Application.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
