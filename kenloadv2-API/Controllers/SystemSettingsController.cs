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

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSettingsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public SystemSettingsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/SystemSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemSettings>>> GetSystemSettings()
        {
            return await _context.SystemSettings.ToListAsync();
        }

        // GET: api/SystemSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemSettings>> GetSystemSettings(int id)
        {
            var SystemSettings = await _context.SystemSettings.FindAsync(id);

            if (SystemSettings == null)
            {
                return NotFound();
            }

            return SystemSettings;
        }

        // PUT: api/Screens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSystemSettings(int id, SystemSettings SystemSettings)
        {
            if (id != SystemSettings.id)
            {
                return BadRequest();
            }

            _context.Entry(SystemSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemSettingsExists(id))
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

        // POST: api/SystemSettings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SystemSettings>> PostSystemSettings(SystemSettings SystemSettings)
        {
            _context.SystemSettings.Add(SystemSettings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSystemSettings", new { id = SystemSettings.id }, SystemSettings);
        }

        // DELETE: api/SystemSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSystemSettings(int id)
        {
            var SystemSettings = await _context.SystemSettings.FindAsync(id);
            if (SystemSettings == null)
            {
                return NotFound();
            }

            _context.SystemSettings.Remove(SystemSettings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SystemSettingsExists(int id)
        {
            return _context.SystemSettings.Any(e => e.id == id);
        }
    }
}
