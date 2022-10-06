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
    public class BackupLogsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public BackupLogsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/BackupLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BackupLogs>>> GetBackupLogs()
        {
            return await _context.BackupLogs.ToListAsync();
        }

        // GET: api/BackupLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BackupLogs>> GetBackupLogs(string id)
        {
            var backupLogs = await _context.BackupLogs.FindAsync(id);

            if (backupLogs == null)
            {
                return NotFound();
            }

            return backupLogs;
        }

        // PUT: api/BackupLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackupLogs(string id, BackupLogs backupLogs)
        {
            if (id != backupLogs.backuptype)
            {
                return BadRequest();
            }

            _context.Entry(backupLogs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackupLogsExists(id))
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

        // POST: api/BackupLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BackupLogs>> PostBackupLogs(BackupLogs backupLogs)
        {
            _context.BackupLogs.Add(backupLogs);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BackupLogsExists(backupLogs.backuptype))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBackupLogs", new { id = backupLogs.backuptype }, backupLogs);
        }

        // DELETE: api/BackupLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackupLogs(string id)
        {
            var backupLogs = await _context.BackupLogs.FindAsync(id);
            if (backupLogs == null)
            {
                return NotFound();
            }

            _context.BackupLogs.Remove(backupLogs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BackupLogsExists(string id)
        {
            return _context.BackupLogs.Any(e => e.backuptype == id);
        }
    }
}
