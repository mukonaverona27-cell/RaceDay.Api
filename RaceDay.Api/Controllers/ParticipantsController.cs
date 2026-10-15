using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ParticipantsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipants()
        {
            return await _context.Participants.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
            var p = await _context.Participants.FindAsync(id);
            if (p == null) return NotFound();
            return p;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant(int id, Participant p)
        {
            if (id != p.ParticipantId) return BadRequest();
            _context.Entry(p).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipant(Participant p)
        {
            _context.Participants.Add(p);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetParticipant), new { id = p.ParticipantId }, p);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            var p = await _context.Participants.FindAsync(id);
            if (p == null) return NotFound();
            _context.Participants.Remove(p);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ParticipantExists(int id) => _context.Participants.Any(e => e.ParticipantId == id);
    }
}