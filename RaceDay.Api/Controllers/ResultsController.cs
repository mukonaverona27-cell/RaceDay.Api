
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
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ResultsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            return await _context.Results.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var result = await _context.Results.FirstOrDefaultAsync(r => r.ResultId == id);
            if (result == null) return NotFound();
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(int id, Result result)
        {
            if (id != result.ResultId) return BadRequest();
            _context.Entry(result).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetResult), new { id = result.ResultId }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null) return NotFound();
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ResultExists(int id) => _context.Results.Any(e => e.ResultId == id);
    }
}