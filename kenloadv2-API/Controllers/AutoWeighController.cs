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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AutoWeighController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public AutoWeighController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/AutoWeigh
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutoWeigh>>> GetAutoWeigh()
        {
            return await _context.AutoWeigh.ToListAsync();
        }

        // GET: api/AutoWeigh/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutoWeigh>> GetAutoWeigh(int id)
        {
            var AutoWeigh = await _context.AutoWeigh.FindAsync(id);

            if (AutoWeigh == null)
            {
                return NotFound();
            }

            return AutoWeigh;
        }

        // PUT: api/AutoWeigh/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutoWeigh(int id, AutoWeigh AutoWeigh)
        {
            if (id != AutoWeigh.id)
            {
                return BadRequest();
            }

            _context.Entry(AutoWeigh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoWeighExists(id))
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

        // POST: api/AutoWeigh
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AutoWeigh>> PostAutoWeigh(AutoWeigh AutoWeigh)
        {
            _context.AutoWeigh.Add(AutoWeigh);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAutoWeigh", new { id = AutoWeigh.id }, AutoWeigh);
        }

        //// DELETE: api/AutoWeigh/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAutoWeigh(int id)
        //{
        //    var AutoWeigh = await _context.AutoWeigh.FindAsync(id);
        //    if (AutoWeigh == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.AutoWeigh.Remove(AutoWeigh);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool AutoWeighExists(int id)
        {
            return _context.AutoWeigh.Any(e => e.id == id);
        }
    }
}
