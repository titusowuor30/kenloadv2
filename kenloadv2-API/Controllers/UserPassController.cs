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
    public class UserPassController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public UserPassController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/UserPass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPass>>> GetUserPass()
        {
            return await _context.UserPass.ToListAsync();
        }

        // GET: api/UserPass/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPass>> GetUserPass(int id)
        {
            var userPass = await _context.UserPass.FindAsync(id);

            if (userPass == null)
            {
                return NotFound();
            }

            return userPass;
        }

        // PUT: api/UserPass/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPass(int id, UserPass userPass)
        {
            if (id != userPass.id)
            {
                return BadRequest();
            }

            _context.Entry(userPass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPassExists(id))
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

        // POST: api/UserPass
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPass>> PostUserPass(UserPass userPass)
        {
            _context.UserPass.Add(userPass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPass", new { id = userPass.id }, userPass);
        }

        // DELETE: api/UserPass/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPass(int id)
        {
            var userPass = await _context.UserPass.FindAsync(id);
            if (userPass == null)
            {
                return NotFound();
            }

            _context.UserPass.Remove(userPass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPassExists(int id)
        {
            return _context.UserPass.Any(e => e.id == id);
        }
    }
}
