using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;
using RaceDay.Web.Models;

namespace RaceDay.Web.Controllers
{
    [Authorize]
    public class ParticipantController : Controller
    {
        private readonly IApiService _api;

        public ParticipantController(IApiService api)
        {
            _api = api;
        }

        // 1. The simple Dashboard
        public IActionResult Dashboard()
        {
            ViewBag.Message = $"Welcome back, {User.Identity?.Name}!";
            return View();
        }

        // 2. The MyEvents page (This will fetch from the API later)
        public async Task<IActionResult> MyEvents()
        {
            try
            {
                var events = await _api.GetAsync<List<Event>>("api/events");
                if (events == null) events = new List<Event>();
                return View(events);
            }
            catch
            {
                return View(new List<Event>());
            }
        }

        // 3. The Apply Form
        [HttpGet]
        public IActionResult Apply(int raceId)
        {
            var model = new RaceEntry { RaceId = raceId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Apply(RaceEntry model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                await _api.PostAsync<RaceEntry>("api/raceentries", model);
                TempData["Success"] = "Application submitted successfully!";
                return RedirectToAction(nameof(MyEvents));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Failed to submit application: " + ex.Message;
                return View(model);
            }
        }
    }
}