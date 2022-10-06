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
    public class CensusController : ControllerBase
    {
        private readonly Kenloadv2Data _context;
        private const string V = "000000";

        public CensusController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Census
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Census>>> GetCensus()
        {
            return await _context.Census.ToListAsync();
        }

        // GET: api/Census/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Census>> GetCensus(int id)
        {
            var Census = await _context.Census.FindAsync(id);

            if (Census == null)
            {
                return NotFound();
            }

            return Census;
        }

        // PUT: api/Census/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCensus(int id, Census Census)
        {
            if (id != Census.id)
            {
                return BadRequest();
            }

            _context.Entry(Census).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CensusExists(id))
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
         [HttpGet("max")]
        public String GetCensusWBTno(String censusid, DateTime fromdate, DateTime todate)
        {
            var ticketno = V;

            if (censusid != null)
            {
                ticketno = _context.Census.Where(x => x.censusid.Substring(0, censusid.Length) == censusid).Count()+"";
                return ticketno;
            }
           

            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                var Selections = _context.Census.Where(x => x.date >= fromdate && x.date <= todate).Count() + "";
                return Selections;
            }

            ticketno = _context.Census.Count() + "";
            
            return ticketno;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Census>>> GetCensusSearch(String shift, String route,  String station, String censusid, String asc, DateTime fromdate, DateTime todate, int tohour, int offset, int limit =50, int fromhour = -1,int deletestatus=-1)
        {
            IQueryable<Census> query = _context.Census;
            var predicate = PredicateBuilder.False<Census>();

            if (shift != null )
            {
                
                if (shift.Contains(","))
                {
                    var rou = shift.Split(",");
                   
                    int clen = rou.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = rou[id];
                        predicate = predicate.Or(x => x.shift == val);
                        id++;
                    }
                   
                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.shift == shift);
                }

            }


            if (station != null)
            {

                if (station.Contains(","))
                {
                    var rou = station.Split(",");

                    int clen = rou.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = rou[id];
                        predicate = predicate.Or(x => x.station == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.station == station);
                }

            }

            if (route != null)
            {
                if (route.Contains(","))
                {
                    var rou = route.Split(",");
                    //query = query.Where(x => x.route == rou[0] || 
                    //                         x.route == rou[1] || 
                    //                         x.route == rou[2] || 
                    //                         x.route == rou[3]);
                    int clen = rou.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String rout = rou[id];
                        predicate = predicate.Or(x => x.route == rout);
                        id++;
                    }
                    //predicate = predicate.Or(x => x.route == rou[0]);
                    //predicate = predicate.Or(x => x.route == rou[1]);
                    //predicate = predicate.Or(x => x.route == rou[2]); 
                    //predicate = predicate.Or(x => x.route == rou[3]);
                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.route == route);
                }
                   
            }
            //if (fromhour>0)
            //{
            //    query = query.Where(x => x.hour >= fromhour && x.hour <= tohour);
            //}


            if (deletestatus >-1)
            {
                query = query.Where(x => x.deletestatus == deletestatus);
            }

            if (censusid != null)
            {
                query = query.Where(x => x.censusid.Substring(0, censusid.Length) == censusid);
            }
            
            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                query = query.Where(x => x.date >= fromdate && x.date <= todate);
            }
             
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (asc != null)
            {
                Selection = await query.OrderBy(a => a.date).OrderBy(a => a.hour).Skip(offset).Take(limit).ToListAsync();
            }
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;
            
        }


        // POST: api/Census
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Census>> PostCensus(Census Census)
        {
            _context.Census.Add(Census);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCensus", new { id = Census.id }, Census);
        }

        // DELETE: api/Census/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCensus(int id)
        {
            var Census = await _context.Census.FindAsync(id);
            if (Census == null)
            {
                return NotFound();
            }

            _context.Census.Remove(Census);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CensusExists(int id)
        {
            return _context.Census.Any(e => e.id == id);
        }
    }
}
