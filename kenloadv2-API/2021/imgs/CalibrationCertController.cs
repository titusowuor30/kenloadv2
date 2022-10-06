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
    public class CalibrationCertController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public CalibrationCertController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/CalibrationCert
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalibrationCert>>> GetCalibrationCert()
        {
            return await _context.CalibrationCert.ToListAsync();
        }

        // GET: api/CalibrationCert/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalibrationCert>> GetCalibrationCert(int id)
        {
            var CalibrationCert = await _context.CalibrationCert.FindAsync(id);

            if (CalibrationCert == null)
            {
                return NotFound();
            }

            return CalibrationCert;
        }

        // PUT: api/CalibrationCert/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalibrationCert(int id, CalibrationCert CalibrationCert)
        {
            if (id != CalibrationCert.id)
            {
                return BadRequest();
            }

            _context.Entry(CalibrationCert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalibrationCertExists(id))
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

        // POST: api/CalibrationCert
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalibrationCert>> PostCalibrationCert(CalibrationCert CalibrationCert)
        {
            _context.CalibrationCert.Add(CalibrationCert);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalibrationCert", new { id = CalibrationCert.id }, CalibrationCert);
        }

        // DELETE: api/CalibrationCert/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalibrationCert(int id)
        {
            var CalibrationCert = await _context.CalibrationCert.FindAsync(id);
            if (CalibrationCert == null)
            {
                return NotFound();
            }

            _context.CalibrationCert.Remove(CalibrationCert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalibrationCertExists(int id)
        {
            return _context.CalibrationCert.Any(e => e.id == id);
        }
    }
}
