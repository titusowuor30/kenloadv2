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
    public class ManualWeighingController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public ManualWeighingController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/ManualWeighing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManualWeighing>>> GetManualWeighing(int offset, int limit = 50)
        { 
            return await _context.ManualWeighing.OrderByDescending(a => a.date).Skip(offset).Take(limit).ToListAsync();
        }

        // GET: api/ManualWeighing/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManualWeighing>> GetManualWeighing(int id)
        {
            var ManualWeighing = await _context.ManualWeighing.FindAsync(id);

            if (ManualWeighing == null)
            {
                return NotFound();
            }

            return ManualWeighing;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ManualWeighing>>> GetManualWeighingSearch(String shift, String route, String station,   DateTime fromdate, DateTime todate,int offset, int deletestatus = -1,  int limit = 50)
        {
            IQueryable<ManualWeighing> query = _context.ManualWeighing;

            
        

            if (station != null)
            {
                query = query.Where(x => x.station == station);
            }

            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                query = query.Where(x => x.date >= fromdate && x.date <= todate);
            }
            if (deletestatus > -1)
            {
                query = query.Where(x => x.deletestatus == deletestatus);
            }

            if (station != null && fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                return await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            }
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        // PUT: api/ManualWeighing/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManualWeighing(int id, ManualWeighing ManualWeighing)
        {
            if (id != ManualWeighing.id)
            {
                return BadRequest();
            }

            _context.Entry(ManualWeighing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManualWeighingExists(id))
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

        // POST: api/ManualWeighing
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ManualWeighing>> PostManualWeighing(ManualWeighing ManualWeighing)
        {
            _context.ManualWeighing.Add(ManualWeighing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManualWeighing", new { id = ManualWeighing.id }, ManualWeighing);
        }

        // DELETE: api/ManualWeighing/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManualWeighing(int id)
        {
            var ManualWeighing = await _context.ManualWeighing.FindAsync(id);
            if (ManualWeighing == null)
            {
                return NotFound();
            }

            _context.ManualWeighing.Remove(ManualWeighing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManualWeighingExists(int id)
        {
            return _context.ManualWeighing.Any(e => e.id == id);
        }
        [HttpGet("hourcountdaterange")]
        public Array GetWeighbridgeTransactionsCount(DateTime fromdate, DateTime todate, int hour = -1)
        {
            int[] hours = new int[24];
            /*   todate = DateTime.Parse(todate);
               fromdate = DateTime.Parse(fromdate);*/

            IQueryable<ManualWeighing> query = _context.ManualWeighing;
            DateTime launchDate = new DateTime(2021, 11, 14, 0, 0, 0);
            DateTime now = DateTime.Now;
            // Calculate the interval between the two dates.  
            TimeSpan ts = launchDate - now;
            TimeSpan daycount = ts;

            for (int h = 0; h < hours.Length; h++)
            {
                hours[h] = query.Where(x => x.date.Hour >= h && x.date >= launchDate).Count();

            }


            /*int hourcont = hours.Length;
            DateTime launchDate = new DateTime(2021, 11, 15, 9, 0, 0);
            DateTime now = DateTime.Now;
            // Calculate the interval between the two dates.  
            TimeSpan ts = launchDate - now;
            TimeSpan daycount = ts;
            int days = 1;
            //int days = int.Parse(daycount.ToString());
            int [,] results = new int[days, hourcont];
            int vehiclecount = 0;
            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                for (int d = 0; d < days; d++)
                {
                    //Add days to date
                    fromdate.AddDays(d);
                    for (int h = 0; h < hours.Length; h++)
                    {
                       hours[h] = query.Where(x => x.wbrg_ticket_date.Hour >= h && x.wbrg_ticket_date >= fromdate).Count();
                       vehiclecount += hours[h];

                    }
                    
                    results[0,0] = vehiclecount;
                    vehiclecount = 0;

                }
            }*/
            return hours;
        }
    }
}
