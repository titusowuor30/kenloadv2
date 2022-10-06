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

   
    public class ProsecutionSettingsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public ProsecutionSettingsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/ProsecutionSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProsecutionSettings>>> GetProsecutionSettings()
        {
            return await _context.ProsecutionSettings.ToListAsync();
        }

        // GET: api/ProsecutionSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProsecutionSettings>> GetProsecutionSettings(int id)
        {
            var ProsecutionSettings = await _context.ProsecutionSettings.FindAsync(id);

            if (ProsecutionSettings == null)
            {
                return NotFound();
            }

            return ProsecutionSettings;
        }

        // PUT: api/Screens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProsecutionSettings(int id, ProsecutionSettings ProsecutionSettings)
        {
            if (id != ProsecutionSettings.id)
            {
                return BadRequest();
            }

            _context.Entry(ProsecutionSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProsecutionSettingsExists(id))
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

        // POST: api/ProsecutionSettings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProsecutionSettings>> PostProsecutionSettings(ProsecutionSettings ProsecutionSettings)
        {
            _context.ProsecutionSettings.Add(ProsecutionSettings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProsecutionSettings", new { id = ProsecutionSettings.id }, ProsecutionSettings);
        }

        // DELETE: api/ProsecutionSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProsecutionSettings(int id)
        {
            var ProsecutionSettings = await _context.ProsecutionSettings.FindAsync(id);
            if (ProsecutionSettings == null)
            {
                return NotFound();
            }

            // _context.ProsecutionSettings.Remove(ProsecutionSettings);
            await _context.SaveChangesAsync();

            return NoContent();
        }
   
        private bool ProsecutionSettingsExists(int id)
        {
            return _context.ProsecutionSettings.Any(e => e.id == id);
        }

        //REPORTS
       
    }
}
