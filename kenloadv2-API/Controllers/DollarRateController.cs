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
    public class DollarRateController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public DollarRateController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/DollarRate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DollarRate>>> GetDollarRate()
        {
            return await _context.DollarRate.ToListAsync();
        }

        // GET: api/DollarRate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DollarRate>> GetDollarRate(int id)
        {
            var DollarRate = await _context.DollarRate.FindAsync(id);

            if (DollarRate == null)
            {
                return NotFound();
            }

            return DollarRate;
        }

        // PUT: api/DollarRate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDollarRate(int id, DollarRate DollarRate)
        //{
        //    if (id != DollarRate.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(DollarRate).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DollarRateExists(id))
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

        // POST: api/DollarRate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DollarRate>> PostDollarRate(DollarRate DollarRate)
        {
            _context.DollarRate.Add(DollarRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDollarRate", new { id = DollarRate.id }, DollarRate);
        }

        //// DELETE: api/DollarRate/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDollarRate(int id)
        //{
        //    var DollarRate = await _context.DollarRate.FindAsync(id);
        //    if (DollarRate == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.DollarRate.Remove(DollarRate);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool DollarRateExists(int id)
        {
            return _context.DollarRate.Any(e => e.id == id);
        }
    }
}
