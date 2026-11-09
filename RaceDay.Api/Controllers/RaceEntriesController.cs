using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Participant,Organiser")]
    public class RaceEntriesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public RaceEntriesController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceEntry>>> GetRaceEntries()
        {
            return await _context.RaceEntries.Include(r => r.Race).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RaceEntry>> GetRaceEntry(int id)
        {
            var entry = await _context.RaceEntries.FindAsync(id);
            if (entry == null) return NotFound();
            return entry;
        }

        [HttpPost]
        public async Task<ActionResult<RaceEntry>> PostRaceEntry(RaceEntry entry)
        {
            // Automatically assign the logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            entry.ParticipantId = userId ?? "";

            _context.RaceEntries.Add(entry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRaceEntry), new { id = entry.RaceEntryId }, entry);
        }

        // (Keep your PutRaceEntry and DeleteRaceEntry methods as they were)
        private bool RaceEntryExists(int id) => _context.RaceEntries.Any(e => e.RaceEntryId == id);
    }
}