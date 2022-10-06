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
    public class AxleWeightsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public AxleWeightsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/AxleWeights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AxleWeights>>> GetAxleWeights(int offset, int limit = 50)
        {
            return await _context.AxleWeights.Skip(offset).Take(limit).ToListAsync(); ;
        }

        // GET: api/AxleWeights/5
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

       
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AxleWeights>>> GetAxleWeightsSearch(String wbrg_ticket_no,int wbtno, int offset, int limit = 50)
        {
            IQueryable<AxleWeights> query = _context.AxleWeights;
            var predicate = PredicateBuilder.False<AxleWeights>();

            if (wbtno != 0)
            {
                query = query.Where(x => x.weighbridgetransactionsid == wbtno);
                
            }
            if (wbrg_ticket_no != null)
            {
                query = query.Where(x => x.wbrg_ticket_no == wbrg_ticket_no);

            }

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderBy(a => a.axle_number).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        [HttpGet("count")]
        public String GetAxleWeightsno(String gvwoverload)
        {
            var ticketno = "000000";
            IQueryable<AxleWeights> query = _context.AxleWeights;
            var predicate = PredicateBuilder.False<AxleWeights>();

            if (gvwoverload != null)
            {
                var Sumgvw = _context.AxleWeights.Where(x => x.wbrg_ticket_no == gvwoverload).Sum(x => x.axle_actualweight);
                var Sumlegalgvw = _context.AxleWeights.Where(x => x.wbrg_ticket_no == gvwoverload).Sum(x => x.axle_permissibleweight);
                ticketno = (Sumgvw - Sumlegalgvw) +"";
                return ticketno;
            }
            
           

            ticketno = _context.WeighbridgeTransactions.Count() + "";

            return ticketno;
        }
        // PUT: api/AxleWeights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAxleWeights(int id, AxleWeights axleWeights)
        //{
        //    if (id != axleWeights.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(axleWeights).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AxleWeightsExists(id))
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
        // POST: api/AxleWeights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AxleWeights>> PostAxleWeights(AxleWeights axleWeights)
        {
            _context.AxleWeights.Add(axleWeights);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAxleWeights", new { id = axleWeights.id }, axleWeights);
        }

        // DELETE: api/AxleWeights/5
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
