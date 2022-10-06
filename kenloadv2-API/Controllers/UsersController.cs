using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenloadV2API.Data;
using KenloadV2API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public UsersController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            // return await _context.Users.Include(userrole=>userrole.userrole).ToListAsync();
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsersSearch(String email, String username, String fullnames, String UserRole, String organization, int offset, int limit = 50)
        {
            IQueryable<Users> query = _context.Users;

            if (email != null)
            {
                //var Selections = await _context.Users.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.email.Substring(0, email.Length) == email);
            }
            if (username != null)
            {
                //var Selections = await _context.Users.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.username == username);
            }
            if (fullnames != null)
            {
                //var Selections = await _context.Users.Where(x => x.wbtno.Substring(0, wbtno.Length) == wbtno).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.fullnames.Substring(0, fullnames.Length) == fullnames);
            }
            if (UserRole != null)
            {
                //var Selections = await _context.Users.Where(x => x.status == status).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.UserRole.Contains(UserRole));
            }
            if (organization != null)
            {
                //var Selections = await _context.Users.Where(x => x.status == status).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.organization == organization);
            }

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            /*var newUser = _mapper.Map<NewPost>(users);
            newUser.userrole = new List<UserRolesCategories>();*/

            //users.userrole = _context.UserRoles.Find(users.userrole.);
            /* var newUserRole = users.userrole;
             if (newUserRole != null)
             {
                 foreach (var userrole in users.userrole)
                 {

                     //some code
                 }
             }*/

            _context.Users.Add(users);
            /* _context.UserRoles.Add(users.userrole);*/
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.id }, users);
        }

        // DISABLE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (id != users.id)
            {
                return BadRequest();
            }
            users.deletedstatus = true;
            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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
        // ENABLE: api/Users/enable/5
        [HttpDelete("enable/{id}")]
        public async Task<IActionResult> EnableUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (id != users.id)
            {
                return BadRequest();
            }
            users.deletedstatus = false;
            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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
        [HttpGet("byMail/{email}")]
        public async Task<ActionResult<Users>> GetUsersByEmail(string email)
        {
            var users = await _context.Users.Where(x => x.email == email).FirstOrDefaultAsync(); ;

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }
    }
}
