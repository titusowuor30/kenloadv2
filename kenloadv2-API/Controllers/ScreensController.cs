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

    class screepass
    {
        public int id { get; set; }
        public int screenid { get; set; }
        public String screenname { get; set; }
        public String acp_pass { get; set; }
        public String username { get; set; }
    }

    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
   


    public class ScreensController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public ScreensController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Screens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Screens>>> GetScreens()
        {
            return await _context.Screens.ToListAsync();
        }

        // GET: api/Screens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Screens>> GetScreens(int id)
        {
            var screens = await _context.Screens.FindAsync(id);

            if (screens == null)
            {
                return NotFound();
            }

            return screens;
        }

        [HttpGet("search")]
        public  Array GetScreenPassSearch(String email, int offset, int limit = 50)
        {
            List<screepass> captureRow = new List<screepass>();
            IQueryable<Screens> query = _context.Screens;

            IQueryable<AccessControlPasswords> queryu = _context.AccessControlPasswords;


            //if (email != null)
            //{
            //    query = query.Where(x => x.screen_name == email);
            //}
         


            //if (wbrg_ticket_no != null || fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") || veh_reg != null || Status != null || transporter != null || cargo != null || caseid != null)
            //{
            //    return await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //}
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);


            query.Skip(offset).Take(limit).ToList();
           
            
            int rowid = 1;

            var access = queryu.ToArray();

            foreach (var row in query)
            {

                var pass = "No Password";
                for (int s = 0; s < access.Length; s++)
                {
                    if (access[s].screenid == row.id )
                    {
                        pass = access[s].acp_pass;
                        s = access.Length + 1;
                    }
                }


                captureRow.Add(
                    new screepass
                    {
                        id = rowid,
                        screenid = row.id,
                        screenname = row.screen_name,
                        acp_pass = pass,
                        username = email,
                    }
                    );
                rowid++;
            }
            return captureRow.ToArray();
          //  return Selection;

        }

        // PUT: api/Screens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScreens(int id, Screens screens)
        {
            if (id != screens.id)
            {
                return BadRequest();
            }

            _context.Entry(screens).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreensExists(id))
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

        // POST: api/Screens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Screens>> PostScreens(Screens screens)
        {
            _context.Screens.Add(screens);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScreens", new { id = screens.id }, screens);
        }

        // DELETE: api/Screens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreens(int id)
        {
            var screens = await _context.Screens.FindAsync(id);
            if (screens == null)
            {
                return NotFound();
            }

            _context.Screens.Remove(screens);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScreensExists(int id)
        {
            return _context.Screens.Any(e => e.id == id);
        }
    }
}
