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
using System.Text.RegularExpressions;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class BackUpDBController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public BackUpDBController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/BackUpDB
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BackUpDB>>> GetBackUpDB()
        {
            return await _context.BackUpDB.Where(x=>x.delete_status==0).ToListAsync();
        }

        // GET: api/BackUpDB/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BackUpDB>> GetBackUpDB(int id)
        {
            var BackUpDB = await _context.BackUpDB.FindAsync(id);

            if (BackUpDB == null)
            {
                return NotFound();
            }

            return BackUpDB;
        }

        
       
        // PUT: api/BackUpDB/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackUpDB(int id, BackUpDB BackUpDB)
        {
            if (id != BackUpDB.id)
            {
                return BadRequest();
            }

            _context.Entry(BackUpDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackUpDBExists(id))
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

        // POST: api/BackUpDB
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BackUpDB>> PostBackUpDB(BackUpDB BackUpDB)
        {
            var autobkName = BackUpDB.backup_name;
            var autobkPath = BackUpDB.backup_path;
            if (autobkName.Contains(".sql"))
            {
                autobkName.Replace(@".sql","").Trim();
            }
            if (autobkName == "")
            {
                autobkName = "kenloadv2Autobackup";
            }
            string clenaedBackupName = string.Empty;
            Regex rgx = new Regex("[^A-Za-z0-9]");
            for (int i = 0; i < autobkName.Length; i++)
            {
                clenaedBackupName += (rgx.IsMatch(autobkName[i].ToString()) ? "_" : autobkName[i].ToString());
            }
            autobkName = clenaedBackupName.Trim();
            if (autobkPath == "")
            {
                autobkPath = "E:\\Kenloadimg\\kenload\\";
            }
            BackUpDB.backup_name = autobkName;  
            BackUpDB.backup_path=autobkPath;
            BackUpDB.backup_time = BackUpDB.backup_time;
            _context.BackUpDB.Add(BackUpDB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBackUpDB", new { id = BackUpDB.id }, BackUpDB);
        }

        // DELETE: api/BackUpDB/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackUpDB(int id)
        {
            var BackUpDB = await _context.BackUpDB.FindAsync(id);
            if (BackUpDB == null)
            {
                return NotFound();
            }
            BackUpDB.delete_status = 1;
            _context.Entry(BackUpDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return NoContent();
        }

        private bool BackUpDBExists(int id)
        {
            return _context.BackUpDB.Any(e => e.id == id);
        }
    }
}
