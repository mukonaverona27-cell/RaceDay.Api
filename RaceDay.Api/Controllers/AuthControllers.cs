using Microsoft.AspNetCore.Mvc;

namespace RaceDay.Api.Controllers
{
    public class Controllers : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
