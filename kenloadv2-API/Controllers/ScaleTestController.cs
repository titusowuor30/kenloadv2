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
    public class ScaleTestController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public ScaleTestController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/ScaleTest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScaleTest>>> GetScaleTest(int offset, int limit = 50)
        {
            return await _context.ScaleTest.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
        }

        // GET: api/ScaleTest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScaleTest>> GetScaleTest(int id)
        {
            var ScaleTest = await _context.ScaleTest.FindAsync(id);

            if (ScaleTest == null)
            {
                return NotFound();
            }

            return ScaleTest;
        }
       
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ScaleTest>>> GetScaleTestSearch( String station, String weighbridge, DateTime fromdate, DateTime todate, int offset, int limit = 50)
        {
           
            IQueryable<ScaleTest> query = _context.ScaleTest;

            if (station != null)
            {
                query = query.Where(x => x.station == station);
            }

            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                query = query.Where(x => x.scaledatetime >= fromdate && x.scaledatetime <= todate);
            }
            if (weighbridge != null)
            {
                query = query.Where(x => x.scalename== weighbridge);
            }

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        // PUT: api/ScaleTest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScaleTest(int id, ScaleTest ScaleTest)
        {
            if (id != ScaleTest.id)
            {
                return BadRequest();
            }

            _context.Entry(ScaleTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScaleTestExists(id))
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

        // POST: api/ScaleTest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScaleTest>> PostScaleTest(ScaleTest ScaleTest)
        {
            _context.ScaleTest.Add(ScaleTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScaleTest", new { id = ScaleTest.id }, ScaleTest);
        }

        // DELETE: api/ScaleTest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScaleTest(int id)
        {
            var ScaleTest = await _context.ScaleTest.FindAsync(id);
            if (ScaleTest == null)
            {
                return NotFound();
            }

            _context.ScaleTest.Remove(ScaleTest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScaleTestExists(int id)
        {
            return _context.ScaleTest.Any(e => e.id == id);
        }
    }
}
