using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RaceDay.Api.Data;
using RaceDay.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Organiser")] // ✅ ONLY Organisers can Create/Edit/Delete Events
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/Events — Anyone can view
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/Events/5 — Anyone can view
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return NotFound();
            return evt;
        }

        // PUT: api/Events/5 — Organiser only
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event evt)
        {
            if (id != evt.EventId) return BadRequest();
            _context.Entry(evt).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        // POST: api/Events — Organiser only
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event evt)
        {
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new { id = evt.EventId }, evt);
        }

        // DELETE: api/Events/5 — Organiser only
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return NotFound();
            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EventExists(int id) => _context.Events.Any(e => e.EventId == id);
    }
}