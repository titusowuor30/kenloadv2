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
    public class HswimController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public HswimController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Hswim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hswim>>> GetHswim()
        {
            return await _context.Hswim.ToListAsync();
        }

        // GET: api/Hswim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hswim>> GetHswim(int id)
        {
            var hswim = await _context.Hswim.FindAsync(id);

            if (hswim == null)
            {
                return NotFound();
            }

            return hswim;
        }

        // PUT: api/Hswim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHswim(int id, Hswim hswim)
        {
            if (id != hswim.id)
            {
                return BadRequest();
            }

            _context.Entry(hswim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HswimExists(id))
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

        // POST: api/Hswim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hswim>> PostHswim(Hswim hswim)
        {
            _context.Hswim.Add(hswim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHswim", new { id = hswim.id }, hswim);
        }

        // DELETE: api/Hswim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHswim(int id)
        {
            var hswim = await _context.Hswim.FindAsync(id);
            if (hswim == null)
            {
                return NotFound();
            }

            _context.Hswim.Remove(hswim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HswimExists(int id)
        {
            return _context.Hswim.Any(e => e.id == id);
        }
    }
}
