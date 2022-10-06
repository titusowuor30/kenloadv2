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
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DeckWeightsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public DeckWeightsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/DeckWeights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeckWeights>>> GetDeckWeights()
        {
            return await _context.DeckWeights.ToListAsync();
        }

        // GET: api/DeckWeights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeckWeights>> GetDeckWeights(string id)
        {
            var deckWeights = await _context.DeckWeights.FindAsync(id);

            if (deckWeights == null)
            {
                return NotFound();
            }

            return deckWeights;
        }

        // PUT: api/DeckWeights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeckWeights(string id, DeckWeights deckWeights)
        {
            if (id != deckWeights.wbrg_name)
            {
                return BadRequest();
            }

            _context.Entry(deckWeights).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeckWeightsExists(id))
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

        // POST: api/DeckWeights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeckWeights>> PostDeckWeights(DeckWeights deckWeights)
        {
            _context.DeckWeights.Add(deckWeights);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeckWeightsExists(deckWeights.wbrg_name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDeckWeights", new { id = deckWeights.wbrg_name }, deckWeights);
        }

        // DELETE: api/DeckWeights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeckWeights(string id)
        {
            var deckWeights = await _context.DeckWeights.FindAsync(id);
            if (deckWeights == null)
            {
                return NotFound();
            }

            _context.DeckWeights.Remove(deckWeights);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeckWeightsExists(string id)
        {
            return _context.DeckWeights.Any(e => e.wbrg_name == id);
        }
    }
}
