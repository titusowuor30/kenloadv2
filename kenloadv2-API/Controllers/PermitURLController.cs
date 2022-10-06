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
    public class PermitURLController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public PermitURLController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/PermitURL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermitURL>>> GetPermitURL()
        {
            return await _context.PermitURL.ToListAsync();
        }

        // GET: api/PermitURL/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PermitURL>> GetPermitURL(int id)
        {
            var PermitURL = await _context.PermitURL.FindAsync(id);

            if (PermitURL == null)
            {
                return NotFound();
            }

            return PermitURL;
        }

        // PUT: api/Screens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermitURL(int id, PermitURL PermitURL)
        {
            if (id != PermitURL.id)
            {
                return BadRequest();
            }

            _context.Entry(PermitURL).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermitURLExists(id))
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

        // POST: api/PermitURL
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PermitURL>> PostPermitURL(PermitURL PermitURL)
        {
            _context.PermitURL.Add(PermitURL);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermitURL", new { id = PermitURL.id }, PermitURL);
        }

        // DELETE: api/PermitURL/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermitURL(int id)
        {
            var PermitURL = await _context.PermitURL.FindAsync(id);
            if (PermitURL == null)
            {
                return NotFound();
            }

            _context.PermitURL.Remove(PermitURL);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermitURLExists(int id)
        {
            return _context.PermitURL.Any(e => e.id == id);
        }
    }
}
