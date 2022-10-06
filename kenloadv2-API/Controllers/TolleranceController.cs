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
    public class TolleranceController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public TolleranceController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Tollerance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tollerance>>> GetTollerance()
        {
            return await _context.Tollerance.OrderByDescending(e => e.id).ToListAsync();
        }

        // GET: api/Tollerance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tollerance>> GetTollerance(int id)
        {
            var Tollerance = await _context.Tollerance.FindAsync(id);

            if (Tollerance == null)
            {
                return NotFound();
            }

            return Tollerance;
        }

        //// PUT: api/Tollerance/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTollerance(int id, Tollerance Tollerance)
        //{
        //    if (id != Tollerance.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(Tollerance).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TolleranceExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Tollerance
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tollerance>> PostTollerance(Tollerance Tollerance)
        {
            _context.Tollerance.Add(Tollerance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTollerance", new { id = Tollerance.id }, Tollerance);
        }

        // DELETE: api/Tollerance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTollerance(int id)
        {
            var Tollerance = await _context.Tollerance.FindAsync(id);
            if (Tollerance == null)
            {
                return NotFound();
            }

            _context.Tollerance.Remove(Tollerance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TolleranceExists(int id)
        {
            return _context.Tollerance.Any(e => e.id == id);
        }
    }
}
