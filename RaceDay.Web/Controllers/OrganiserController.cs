using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RaceDay.Web.Controllers
{
    [Authorize] // Temporarily bypassed role restriction
    public class OrganiserController : Controller
    {
        public IActionResult Dashboard() => View();
    }
}