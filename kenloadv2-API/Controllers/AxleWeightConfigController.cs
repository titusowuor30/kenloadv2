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
    public class AxleWeightConfigController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public AxleWeightConfigController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/AxleWeightConfig
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AxleWeightConfig>>> GetAxleWeightConfig()
        {
            return await _context.AxleWeightConfig.OrderBy(a => a.axle_code).ToListAsync();
        }
        // GET: api/AxleWeightConfig/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AxleWeightConfig>> GetAxleWeightConfig(int id)
        {
            var axleWeightConfig = await _context.AxleWeightConfig.FindAsync(id);

            if (axleWeightConfig == null)
            {
                return NotFound();
            }

            return axleWeightConfig;
        }

        // PUT: api/AxleWeightConfig/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAxleWeightConfig(int id, AxleWeightConfig axleWeightConfig)
        {
            if (id != axleWeightConfig.id)
            {
                return BadRequest();
            }

            _context.Entry(axleWeightConfig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AxleWeightConfigExists(id))
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

        // POST: api/AxleWeightConfig
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AxleWeightConfig>> PostAxleWeightConfig(AxleWeightConfig axleWeightConfig)
        {
            _context.AxleWeightConfig.Add(axleWeightConfig);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAxleWeightConfig", new { id = axleWeightConfig.id }, axleWeightConfig);
        }

        [HttpGet("report/axleconfig")]
        public Array axleconfig_report(int limit = 50)
        {
            var query = (from a in _context.Set<AxleWeightConfig>()
                         from b in _context.Set<AxleWeightXreff>().Where(b => b.axle_code == a.axle_code)

                         select new { a.id, a.axle_name, b.axle_deckgrouping, b.axle_group, b.axle_legalweight }).Distinct();
            //return query.ToArray();
            //IQueryable<AxleWeightConfig> query = _context.AxleWeightConfig.Include(axleweightxreff => axleweightxreff.axle_code);


            //query = query.Where(x => x.datetime >= fromdate && x.datetime <= todate);
            var Selection = query.Take(limit).ToList();
            // if (Selection == null)
            // {
            //     return NotFound().ToArray();
            // }
            return Selection.ToArray();
        }


        // DELETE: api/AxleWeightConfig/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAxleWeightConfig(int id)
        {
            var axleWeightConfig = await _context.AxleWeightConfig.FindAsync(id);
            if (axleWeightConfig == null)
            {
                return NotFound();
            }

            _context.AxleWeightConfig.Remove(axleWeightConfig);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AxleWeightConfigExists(int id)
        {
            return _context.AxleWeightConfig.Any(e => e.id == id);
        }
    }
}
