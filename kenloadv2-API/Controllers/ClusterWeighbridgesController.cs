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
    public class ClusterWeighbridgesController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public ClusterWeighbridgesController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/ClusterWeighbridges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClusterWeighbridge>>> GetClusterWeighbridges()
        {
            return await _context.ClusterWeighbridges.ToListAsync();
        }

        // GET: api/ClusterWeighbridges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClusterWeighbridge>> GetClusterWeighbridge(int id)
        {
            var clusterWeighbridge = await _context.ClusterWeighbridges.FindAsync(id);

            if (clusterWeighbridge == null)
            {
                return NotFound();
            }

            return clusterWeighbridge;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ClusterWeighbridge>>> GetCaseDetailsSearch(String cluster_code, String cluster_name,  int offset, int limit = 50)
        {

            if (cluster_code != null)
            {
                var Selections = await _context.ClusterWeighbridges.Where(x => x.cluster_code.Substring(0, cluster_code.Length) == cluster_code).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (cluster_name != null)
            {
                var Selections = await _context.ClusterWeighbridges.Where(x => x.cluster_name.Substring(0, cluster_name.Length) == cluster_name).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await _context.ClusterWeighbridges.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }

        // PUT: api/ClusterWeighbridges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClusterWeighbridge(int id, ClusterWeighbridge clusterWeighbridge)
        {
            if (id != clusterWeighbridge.id)
            {
                return BadRequest();
            }

            _context.Entry(clusterWeighbridge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClusterWeighbridgeExists(id))
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

        // POST: api/ClusterWeighbridges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClusterWeighbridge>> PostClusterWeighbridge(ClusterWeighbridge clusterWeighbridge)
        {
            _context.ClusterWeighbridges.Add(clusterWeighbridge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClusterWeighbridge", new { id = clusterWeighbridge.id }, clusterWeighbridge);
        }

        // DELETE: api/ClusterWeighbridges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClusterWeighbridge(int id)
        {
            var clusterWeighbridge = await _context.ClusterWeighbridges.FindAsync(id);
            if (clusterWeighbridge == null)
            {
                return NotFound();
            }

            _context.ClusterWeighbridges.Remove(clusterWeighbridge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClusterWeighbridgeExists(int id)
        {
            return _context.ClusterWeighbridges.Any(e => e.id == id);
        }
    }
}
