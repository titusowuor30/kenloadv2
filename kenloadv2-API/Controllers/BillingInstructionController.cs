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
    public class BillingInstructionController : ControllerBase
    {
        private const string V = "000000";
        private readonly Kenloadv2Data _context;

        public BillingInstructionController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/BillingInstruction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillingInstruction>>> GetBillingInstruction(int offset, int limit = 50)
        {
            
            return await _context.BillingInstruction.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
        }

        // GET: api/BillingInstruction/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BillingInstruction>> GetBillingInstruction(int id)
        {
            var BillingInstruction = await _context.BillingInstruction.FindAsync(id);

            if (BillingInstruction == null)
            {
                return NotFound();
            }

            return BillingInstruction;
        }

       
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BillingInstruction>>> GetBillingInstructionSearch(String vehreg, String billingid, String caseid,  int offset, int limit = 50)
        {

            if (caseid != null)
            {
                 var Selections = await _context.BillingInstruction.Where(x => x.caseid.Substring(0, caseid.Length) == caseid).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
           
            if (billingid != null)
            {
                var Selections = await _context.BillingInstruction.Where(x => x.caseid.Substring(0, billingid.Length) == billingid).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
           
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await _context.BillingInstruction.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        [HttpGet("max")]
        public String GetBillingInstructionWBTno(String billingid)
        {
            var ticketno = V;

            if (billingid != null)
            {
                ticketno = _context.BillingInstruction.Where(x => x.billingid.Substring(0, billingid.Length) == billingid).Max(x => x.billingid.Substring(billingid.Length, billingid.Length + 6));
            }


            if (ticketno == null)
            {
                return "000000";
            }
            return ticketno;
        }

        // PUT: api/BillingInstruction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillingInstruction(int id, BillingInstruction BillingInstruction)
        {
            if (id != BillingInstruction.id)
            {
                return BadRequest();
            }

            _context.Entry(BillingInstruction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingInstructionExists(id))
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

        // POST: api/BillingInstruction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BillingInstruction>> PostBillingInstruction(BillingInstruction BillingInstruction)
        {
            _context.BillingInstruction.Add(BillingInstruction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillingInstruction", new { id = BillingInstruction.id }, BillingInstruction);
        }

        // DELETE: api/BillingInstruction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillingInstruction(int id)
        {
            var BillingInstruction = await _context.BillingInstruction.FindAsync(id);
            if (BillingInstruction == null)
            {
                return NotFound();
            }

            _context.BillingInstruction.Remove(BillingInstruction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillingInstructionExists(int id)
        {
            return _context.BillingInstruction.Any(e => e.id == id);
        }
        
    }
}
