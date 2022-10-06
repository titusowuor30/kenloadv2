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
    public class AxleWeightController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public AxleWeightController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/AxleWeight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AxleWeights>>> GetAxleWeights()
        {
            return await _context.AxleWeights.ToListAsync();
        }

        // GET: api/AxleWeight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AxleWeights>> GetAxleWeights(int id)
        {
            var axleWeights = await _context.AxleWeights.FindAsync(id);

            if (axleWeights == null)
            {
                return NotFound();
            }

            return axleWeights;
        }

        // PUT: api/AxleWeight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAxleWeights(int id, AxleWeights axleWeights)
        {
            if (id != axleWeights.id)
            {
                return BadRequest();
            }

            _context.Entry(axleWeights).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AxleWeightsExists(id))
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

        // POST: api/AxleWeight
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AxleWeights>> PostAxleWeights(AxleWeights axleWeights)
        {
            _context.AxleWeights.Add(axleWeights);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAxleWeights", new { id = axleWeights.id }, axleWeights);
        }

        // DELETE: api/AxleWeight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAxleWeights(int id)
        {
            var axleWeights = await _context.AxleWeights.FindAsync(id);
            if (axleWeights == null)
            {
                return NotFound();
            }

            _context.AxleWeights.Remove(axleWeights);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AxleWeightsExists(int id)
        {
            return _context.AxleWeights.Any(e => e.id == id);
        }
    }
}
