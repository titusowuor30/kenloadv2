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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserRoleCombinationController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public UserRoleCombinationController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/UserRoleCombination
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleCombination>>> GetUserRoleCombination()
        {
            return await _context.UserRoleCombination.ToListAsync();
        }

        // GET: api/UserRoleCombination/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleCombination>> GetUserRoleCombination(int id)
        {
            var userRoleCombination = await _context.UserRoleCombination.FindAsync(id);

            if (userRoleCombination == null)
            {
                return NotFound();
            }

            return userRoleCombination;
        }

        // PUT: api/UserRoleCombination/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRoleCombination(int id, UserRoleCombination userRoleCombination)
        {
            if (id != userRoleCombination.id)
            {
                return BadRequest();
            }

            _context.Entry(userRoleCombination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleCombinationExists(id))
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

        // POST: api/UserRoleCombination
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRoleCombination>> PostUserRoleCombination(UserRoleCombination userRoleCombination)
        {
            _context.UserRoleCombination.Add(userRoleCombination);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserRoleCombination", new { id = userRoleCombination.id }, userRoleCombination);
        }

        // DELETE: api/UserRoleCombination/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRoleCombination(int id)
        {
            var userRoleCombination = await _context.UserRoleCombination.FindAsync(id);
            if (userRoleCombination == null)
            {
                return NotFound();
            }

            _context.UserRoleCombination.Remove(userRoleCombination);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRoleCombinationExists(int id)
        {
            return _context.UserRoleCombination.Any(e => e.id == id);
        }
    }
}
