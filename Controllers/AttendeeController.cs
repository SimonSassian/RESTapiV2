using Microsoft.AspNetCore.Mvc;

namespace ITB2203Application.Controllers
{
    public class AttendeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
