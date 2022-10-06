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
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccessPassPolicyController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public AccessPassPolicyController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/AccessPassPolicy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessPassPolicy>>> GetAccessPassPolicy()
        {
            return await _context.AccessPassPolicy.ToListAsync();
        }

        // GET: api/AccessPassPolicy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessPassPolicy>> GetAccessPassPolicy(int id)
        {
            var accessPassPolicy = await _context.AccessPassPolicy.FindAsync(id);

            if (accessPassPolicy == null)
            {
                return NotFound();
            }

            return accessPassPolicy;
        }

        // PUT: api/AccessPassPolicy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessPassPolicy(int id, AccessPassPolicy accessPassPolicy)
        {
            if (id != accessPassPolicy.id)
            {
                return BadRequest();
            }

            _context.Entry(accessPassPolicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessPassPolicyExists(id))
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

        // POST: api/AccessPassPolicy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccessPassPolicy>> PostAccessPassPolicy(AccessPassPolicy accessPassPolicy)
        {
            _context.AccessPassPolicy.Add(accessPassPolicy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccessPassPolicy", new { id = accessPassPolicy.id }, accessPassPolicy);
        }

        // DELETE: api/AccessPassPolicy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessPassPolicy(int id)
        {
            var accessPassPolicy = await _context.AccessPassPolicy.FindAsync(id);
            if (accessPassPolicy == null)
            {
                return NotFound();
            }

            _context.AccessPassPolicy.Remove(accessPassPolicy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccessPassPolicyExists(int id)
        {
            return _context.AccessPassPolicy.Any(e => e.id == id);
        }
    }
}
