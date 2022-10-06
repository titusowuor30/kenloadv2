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
    public class AxleWeightXreffsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public AxleWeightXreffsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/AxleWeightXreffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AxleWeightXreff>>> GetAxleWeightXreff()
        {
            return await _context.AxleWeightXreff.OrderBy(x => x.axle_code2).ToListAsync();
        }

        // GET: api/AxleWeightXreffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AxleWeightXreff>> GetAxleWeightXreff(int id)
        {
            var axleWeightXreff = await _context.AxleWeightXreff.FindAsync(id);

            if (axleWeightXreff == null)
            {
                return NotFound();
            }

            return axleWeightXreff;
        }

        // PUT: api/AxleWeightXreffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAxleWeightXreff(int id, AxleWeightXreff axleWeightXreff)
        {
            if (id != axleWeightXreff.id)
            {
                return BadRequest();
            }

            _context.Entry(axleWeightXreff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AxleWeightXreffExists(id))
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

        // POST: api/AxleWeightXreffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AxleWeightXreff>> PostAxleWeightXreff(AxleWeightXreff axleWeightXreff)
        {
            _context.AxleWeightXreff.Add(axleWeightXreff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAxleWeightXreff", new { id = axleWeightXreff.id }, axleWeightXreff);
        }

        // DELETE: api/AxleWeightXreffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAxleWeightXreff(int id)
        {
            var axleWeightXreff = await _context.AxleWeightXreff.FindAsync(id);
            if (axleWeightXreff == null)
            {
                return NotFound();
            }

            _context.AxleWeightXreff.Remove(axleWeightXreff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AxleWeightXreffExists(int id)
        {
            return _context.AxleWeightXreff.Any(e => e.id == id);
        }
    }
}
