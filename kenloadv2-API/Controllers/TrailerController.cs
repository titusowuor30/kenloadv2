using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenloadV2API.Data;
using KenloadV2API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TrailerController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public TrailerController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Trailer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trailer>>> GetTrailer()
        {
            return await _context.Trailer.ToListAsync();
        }

        // GET: api/Trailer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trailer>> GetTrailer(int id)
        {
            var trailer = await _context.Trailer.FindAsync(id);

            if (trailer == null)
            {
                return NotFound();
            }

            return trailer;
        }

        // PUT: api/Trailer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrailer(int id, Trailer trailer)
        {
            if (id != trailer.id)
            {
                return BadRequest();
            }

            _context.Entry(trailer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrailerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trailer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trailer>> PostTrailer(Trailer trailer)
        {
            _context.Trailer.Add(trailer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrailer", new { id = trailer.id }, trailer);
        }

        // DELETE: api/Trailer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrailer(int id)
        {
            var trailer = await _context.Trailer.FindAsync(id);
            if (trailer == null)
            {
                return NotFound();
            }

            _context.Trailer.Remove(trailer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrailerExists(int id)
        {
            return _context.Trailer.Any(e => e.id == id);
        }
    }
}
