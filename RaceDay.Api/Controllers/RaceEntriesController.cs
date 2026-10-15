using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Participant,Organiser")]
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
            return await _context.RaceEntries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RaceEntry>> GetRaceEntry(int id)
        {
            var entry = await _context.RaceEntries.FirstOrDefaultAsync(e => e.RaceEntryId == id);
            if (entry == null) return NotFound();
            return entry;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRaceEntry(int id, RaceEntry entry)
        {
            if (id != entry.RaceEntryId) return BadRequest();
            _context.Entry(entry).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceEntryExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<RaceEntry>> PostRaceEntry(RaceEntry entry)
        {
            _context.RaceEntries.Add(entry);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRaceEntry), new { id = entry.RaceEntryId }, entry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRaceEntry(int id)
        {
            var entry = await _context.RaceEntries.FindAsync(id);
            if (entry == null) return NotFound();
            _context.RaceEntries.Remove(entry);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool RaceEntryExists(int id) => _context.RaceEntries.Any(e => e.RaceEntryId == id);
    }
}