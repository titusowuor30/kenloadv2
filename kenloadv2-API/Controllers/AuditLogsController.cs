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
    public class AuditLogsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public AuditLogsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/AuditLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLogs>>> GetAuditLogs()
        {
            int limit = 50;
            return await _context.AuditLogs.Take(limit).ToListAsync();
                }
        //search
         [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AuditLogs>>> GetAuditLogsSearch(int limit = 50)
        {
            return await _context.AuditLogs.OrderByDescending(x => x.id).Take(limit).ToListAsync();
        }
        // GET: api/AuditLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuditLogs>> GetAuditLogs(int id)
        {
            var AuditLogs = await _context.AuditLogs.FindAsync(id);

            if (AuditLogs == null)
            {
                return NotFound();
            }

            return AuditLogs;
        }

        // PUT: api/AuditLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuditLogs(int id, AuditLogs AuditLogs)
        {
            if (id != AuditLogs.id)
            {
                return BadRequest();
            }

            _context.Entry(AuditLogs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogsExists(id))
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

        // POST: api/AuditLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuditLogs>> PostAuditLogs(AuditLogs AuditLogs)
        {
            _context.AuditLogs.Add(AuditLogs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuditLogs", new { id = AuditLogs.id }, AuditLogs);
        }

        // DELETE: api/AuditLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuditLogs(int id)
        {
            var AuditLogs = await _context.AuditLogs.FindAsync(id);
            if (AuditLogs == null)
            {
                return NotFound();
            }

            _context.AuditLogs.Remove(AuditLogs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuditLogsExists(int id)
        {
            return _context.AuditLogs.Any(e => e.id == id);
        }
    }
}
