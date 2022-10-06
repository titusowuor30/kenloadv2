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
    public class ControlSelectionController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public ControlSelectionController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/ControlSelection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControlSelection>>> GetControlSelections()
        {
            return await _context.ControlSelections.ToListAsync();
        }

        // GET: api/ControlSelection/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlSelection>> GetControlSelection(int id)
        {
            var controlSelection = await _context.ControlSelections.FindAsync(id);

            if (controlSelection == null)
            {
                return NotFound();
            }

            return controlSelection;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ControlSelection>>> GetControlSelections(int roleid)
        {
            var controlSelection = await _context.ControlSelections.Where(x => x.roleid == roleid).ToListAsync();
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);

            if (controlSelection == null)
            {
                return NotFound();
            }

            return controlSelection;
        }

        // PUT: api/ControlSelection/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlSelection(int id, ControlSelection controlSelection)
        {
            if (id != controlSelection.id)
            {
                return BadRequest();
            }

            _context.Entry(controlSelection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlSelectionExists(id))
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

        // POST: api/ControlSelection
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ControlSelection>> PostControlSelection(ControlSelection controlSelection)
        {
            _context.ControlSelections.Add(controlSelection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControlSelection", new { id = controlSelection.id }, controlSelection);
        }

        // DELETE: api/ControlSelection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControlSelection(int id)
        {
            var controlSelection = await _context.ControlSelections.FindAsync(id);
            if (controlSelection == null)
            {
                return NotFound();
            }

            _context.ControlSelections.Remove(controlSelection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteControlSelections(int roleid)
        {
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var controlSelection = await _context.ControlSelections.Where(x => x.roleid == roleid).FirstOrDefaultAsync();
            if (controlSelection == null)
            {
                return NotFound();
            }
            while (controlSelection != null)
            {
                controlSelection = await _context.ControlSelections.Where(x => x.roleid == roleid).FirstOrDefaultAsync();
            if (controlSelection == null)
            {
                return NotFound();
            }
            
                _context.ControlSelections.Remove(controlSelection);
            await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool ControlSelectionExists(int id)
        {
            return _context.ControlSelections.Any(e => e.id == id);
        }
    }
}
