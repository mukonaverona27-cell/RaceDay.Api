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
    public class RacesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public RacesController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> GetRaces()
        {
            return await _context.Races.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Race>> GetRace(int id)
        {
            var race = await _context.Races.FirstOrDefaultAsync(r => r.RaceId == id);
            if (race == null) return NotFound();
            return race;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRace(int id, Race race)
        {
            if (id != race.RaceId) return BadRequest();
            _context.Entry(race).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Race>> PostRace(Race race)
        {
            _context.Races.Add(race);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRace), new { id = race.RaceId }, race);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var race = await _context.Races.FindAsync(id);
            if (race == null) return NotFound();
            _context.Races.Remove(race);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool RaceExists(int id) => _context.Races.Any(e => e.RaceId == id);
    }
}