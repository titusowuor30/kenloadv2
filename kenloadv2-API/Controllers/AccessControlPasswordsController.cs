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
using System.Security.Cryptography;
using System.Text;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccessControlPasswordsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public AccessControlPasswordsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/AccessControlPasswords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessControlPasswords>>> GetAccessControlPasswords()
        {
            return await _context.AccessControlPasswords.ToListAsync();
        }

        // GET: api/AccessControlPasswords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessControlPasswords>> GetAccessControlPasswords(int id)
        {
            var accessControlPasswords = await _context.AccessControlPasswords.FindAsync(id);

            if (accessControlPasswords == null)
            {
                return NotFound();
            }

            return accessControlPasswords;
        }
        [HttpGet("grant")]
        public Boolean GetGrantSearch(String email, String pass)
        {
            IQueryable<AccessControlPasswords> query = _context.AccessControlPasswords;
            var encpass = "";
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            Convert.ToBase64String(inArray);
            encpass = Convert.ToBase64String(inArray);

            if (email != null)
            {
                //var Selections = await _context.Users.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.username.Substring(0, email.Length) == email && x.acp_pass == encpass);
            }

           

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = query.ToArray();
            if (Selection.Length <=0 )
            {
                return false;
            }
            return true;

        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AccessControlPasswords>>> GetAccessControlPasswordsSearch(String email,int screenid, int offset, int limit = 50)
        {
            IQueryable<AccessControlPasswords> query = _context.AccessControlPasswords;

            if (email != null)
            {
                //var Selections = await _context.Users.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.username.Substring(0, email.Length) == email);
            }

            if (screenid >0)
            {
                //var Selections = await _context.Users.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.screenid== screenid);
            }

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        // PUT: api/AccessControlPasswords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessControlPasswords(int id, AccessControlPasswords accessControlPasswords)
        {
            if (id != accessControlPasswords.id)
            {
                return BadRequest();
            }

            _context.Entry(accessControlPasswords).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessControlPasswordsExists(id))
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

        // POST: api/AccessControlPasswords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccessControlPasswords>> PostAccessControlPasswords(AccessControlPasswords accessControlPasswords)
        {
            IQueryable<AccessControlPasswords> queryu = _context.AccessControlPasswords;

            var users = queryu.Where(a => a.username == accessControlPasswords.username && a.screenid == accessControlPasswords.screenid).ToArray();

            for (int s = 0; s < users.Length; s++)
            {
                var id = users[s].id;
                var accessControlPassword = await _context.AccessControlPasswords.FindAsync(id);
                if (accessControlPassword == null)
                {
                    return NotFound();
                }

                _context.AccessControlPasswords.Remove(accessControlPassword);
                await _context.SaveChangesAsync();
            }
           




            byte[] bytes = Encoding.Unicode.GetBytes(accessControlPasswords.acp_pass);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
             Convert.ToBase64String(inArray);
            accessControlPasswords.acp_pass= Convert.ToBase64String(inArray);

            _context.AccessControlPasswords.Add(accessControlPasswords);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccessControlPasswords", new { id = accessControlPasswords.id }, accessControlPasswords);
        }

        // DELETE: api/AccessControlPasswords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessControlPasswords(int id)
        {
            var accessControlPasswords = await _context.AccessControlPasswords.FindAsync(id);
            if (accessControlPasswords == null)
            {
                return NotFound();
            }

            _context.AccessControlPasswords.Remove(accessControlPasswords);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccessControlPasswordsExists(int id)
        {
            return _context.AccessControlPasswords.Any(e => e.id == id);
        }
    }
}
