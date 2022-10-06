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
    public class GVWoverloadChargesController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public GVWoverloadChargesController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/GVWoverloadCharges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GVWoverloadCharges>>> GetGVWoverloadCharges()
        {
            return await _context.GVWoverloadCharges.ToListAsync();
        }

        // GET: api/GVWoverloadCharges/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GVWoverloadCharges>> GetGVWoverloadCharges(int id)
        {
            var GVWoverloadCharges = await _context.GVWoverloadCharges.FindAsync(id);

            if (GVWoverloadCharges == null)
            {
                return NotFound();
            }

            return GVWoverloadCharges;
        }

        
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<GVWoverloadCharges>>> GetGVWoverloadChargesSearch(String period, int offset, int limit = 50, int overloadkg = -1)
        {
            IQueryable<GVWoverloadCharges> query = _context.GVWoverloadCharges;
            
            if (period != null)
            {
                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.period.Substring(0, period.Length) != period);
            }
            if (overloadkg >=0)
            {
                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.overloadkg == overloadkg);
            }
           
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
                var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        // PUT: api/GVWoverloadCharges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGVWoverloadCharges(int id, GVWoverloadCharges GVWoverloadCharges)
        //{
        //    if (id != GVWoverloadCharges.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(GVWoverloadCharges).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GVWoverloadChargesExists(id))
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

        //// POST: api/GVWoverloadCharges
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<GVWoverloadCharges>> PostGVWoverloadCharges(GVWoverloadCharges GVWoverloadCharges)
        //{
        //    _context.GVWoverloadCharges.Add(GVWoverloadCharges);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetGVWoverloadCharges", new { id = GVWoverloadCharges.id }, GVWoverloadCharges);
        //}

        // DELETE: api/GVWoverloadCharges/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGVWoverloadCharges(int id)
        //{
        //    var GVWoverloadCharges = await _context.GVWoverloadCharges.FindAsync(id);
        //    if (GVWoverloadCharges == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.GVWoverloadCharges.Remove(GVWoverloadCharges);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool GVWoverloadChargesExists(int id)
        {
            return _context.GVWoverloadCharges.Any(e => e.id == id);
        }
    }
}
