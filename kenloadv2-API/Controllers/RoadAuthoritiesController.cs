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
    public class RoadAuthoritiesController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public RoadAuthoritiesController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/RoadAuthorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoadAuthority>>> GetRoadAuthority()
        {
            return await _context.RoadAuthority.ToListAsync();
        }

        // GET: api/RoadAuthorities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoadAuthority>> GetRoadAuthority(int id)
        {
            var roadAuthority = await _context.RoadAuthority.FindAsync(id);

            if (roadAuthority == null)
            {
                return NotFound();
            }

            return roadAuthority;
        }

        // PUT: api/RoadAuthorities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoadAuthority(int id, RoadAuthority roadAuthority)
        {
            if (id != roadAuthority.id)
            {
                return BadRequest();
            }

            _context.Entry(roadAuthority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoadAuthorityExists(id))
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

        // POST: api/RoadAuthorities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoadAuthority>> PostRoadAuthority(RoadAuthority roadAuthority)
        {
            _context.RoadAuthority.Add(roadAuthority);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoadAuthority", new { id = roadAuthority.id }, roadAuthority);
        }

        // DELETE: api/RoadAuthorities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoadAuthority(int id)
        {
            var roadAuthority = await _context.RoadAuthority.FindAsync(id);
            if (roadAuthority == null)
            {
                return NotFound();
            }

            _context.RoadAuthority.Remove(roadAuthority);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoadAuthorityExists(int id)
        {
            return _context.RoadAuthority.Any(e => e.id == id);
        }
    }
}
