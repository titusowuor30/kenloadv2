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
    public class OriginsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public OriginsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Origins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Origin>>> GetOrigin()
        {
            return await _context.Origin.ToListAsync();
        }

        // GET: api/Origins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Origin>> GetOrigin(int id)
        {
            var origin = await _context.Origin.FindAsync(id);

            if (origin == null)
            {
                return NotFound();
            }

            return origin;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Origin>>> GetOriginSearch(String Origin, int offset, int limit = 50)
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

            if (Origin != null)
            {
                var Selections = await _context.Origin.Where(x => x.origin_name == Origin).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
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
            var Selection = await _context.Origin.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        // PUT: api/Origins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrigin(int id, Origin origin)
        {
            if (id != origin.id)
            {
                return BadRequest();
            }

            _context.Entry(origin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OriginExists(id))
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

        // POST: api/Origins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Origin>> PostOrigin(Origin origin)
        {
            _context.Origin.Add(origin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrigin", new { id = origin.id }, origin);
        }

        // DELETE: api/Origins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrigin(int id)
        {
            var origin = await _context.Origin.FindAsync(id);
            if (origin == null)
            {
                return NotFound();
            }

            _context.Origin.Remove(origin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OriginExists(int id)
        {
            return _context.Origin.Any(e => e.id == id);
        }
    }
}
