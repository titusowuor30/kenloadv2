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
    public class AVWoverloadChargesController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public AVWoverloadChargesController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/AVWoverloadCharges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AVWoverloadCharges>>> GetAVWoverloadCharges(int offset, int limit = 50)
        {
            return await _context.AVWoverloadCharges.OrderByDescending(a => a.overloadkg).Skip(offset).Take(limit).ToListAsync(); ;
        }

        // GET: api/AVWoverloadCharges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AVWoverloadCharges>> GetAVWoverloadCharges(int id)
        {
            var AVWoverloadCharges = await _context.AVWoverloadCharges.FindAsync(id);

            if (AVWoverloadCharges == null)
            {
                return NotFound();
            }

            return AVWoverloadCharges;
        }

       
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AVWoverloadCharges>>> GetAVWoverloadChargesSearch( int singledriveaxle, int steeringaxle, int tridemaxle, int tandemaxle, int offset, int limit = 50)
        {
            IQueryable<AVWoverloadCharges> query = _context.AVWoverloadCharges;
            var predicate = PredicateBuilder.False<AVWoverloadCharges>();

            if (steeringaxle != 0)
            {
                query = query.Where(x => x.steeringaxle == steeringaxle);
                
            }
            if (singledriveaxle != 0)
            {
                query = query.Where(x => x.singledriveaxle == singledriveaxle);

            }
            if (tandemaxle != 0)
            {
                query = query.Where(x => x.tandemaxle == tandemaxle);

            }
            if (tridemaxle != 0)
            {
                query = query.Where(x => x.tridemaxle == tridemaxle);

            }


            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        
        // PUT: api/AVWoverloadCharges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAVWoverloadCharges(int id, AVWoverloadCharges AVWoverloadCharges)
        //{
        //    if (id != AVWoverloadCharges.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(AVWoverloadCharges).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AVWoverloadChargesExists(id))
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
        // POST: api/AVWoverloadCharges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AVWoverloadCharges>> PostAVWoverloadCharges(AVWoverloadCharges AVWoverloadCharges)
        {
            _context.AVWoverloadCharges.Add(AVWoverloadCharges);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAVWoverloadCharges", new { id = AVWoverloadCharges.id }, AVWoverloadCharges);
        }

        // DELETE: api/AVWoverloadCharges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAVWoverloadCharges(int id)
        {
            var AVWoverloadCharges = await _context.AVWoverloadCharges.FindAsync(id);
            if (AVWoverloadCharges == null)
            {
                return NotFound();
            }

            _context.AVWoverloadCharges.Remove(AVWoverloadCharges);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AVWoverloadChargesExists(int id)
        {
            return _context.AVWoverloadCharges.Any(e => e.id == id);
        }
    }
}
