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
    public class TransportersController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public TransportersController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Transporters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transporters>>> GetTransporters(int offset, int limit = 50)
        {
            return await _context.Transporters.Skip(offset).Take(limit).ToListAsync();
        }

        // GET: api/Transporters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transporters>> GetTransporters(int id)
        {
            var transporters = await _context.Transporters.FindAsync(id);

            if (transporters == null)
            {
                return NotFound();
            }

            return transporters;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Transporters>>> GetTransportersSearch(String traname, int offset, int limit = 50)
        {

            //if (status != null && vehreg != null && caseid != null)
            //{
            //    var Selections = await _context.YardList.Where(x => x.vehreg == vehreg && x.status == status && x.caseid.Substring(0, caseid.Length) != caseid).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //    return Selections;
            //}
            //if (status != null && vehreg != null)
            //{
            //    var Selections = await _context.YardList.Where(x => x.vehreg == vehreg && x.status == status).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //    return Selections;
            //}

            if (traname != null)
            {
                var Selections = await _context.Transporters.Where(x => x.transporter_name == traname).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            //if (wbtno != null)
            //{
            //    var Selections = await _context.YardList.Where(x => x.wbtno.Substring(0, wbtno.Length) == wbtno).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //    return Selections;
            //}
            //if (status != null)
            //{
            //    var Selections = await _context.YardList.Where(x => x.status == status).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //    return Selections;
            //}
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await _context.Transporters.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }

        // PUT: api/Transporters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransporters(int id, Transporters transporters)
        {
            if (id != transporters.id)
            {
                return BadRequest();
            }

            _context.Entry(transporters).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportersExists(id))
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

        // POST: api/Transporters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transporters>> PostTransporters(Transporters transporters)
        {
            _context.Transporters.Add(transporters);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransporters", new { id = transporters.id }, transporters);
        }

        // DELETE: api/Transporters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporters(int id)
        {
            var transporters = await _context.Transporters.FindAsync(id);
            if (transporters == null)
            {
                return NotFound();
            }

            _context.Transporters.Remove(transporters);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransportersExists(int id)
        {
            return _context.Transporters.Any(e => e.id == id);
        }
    }
}
