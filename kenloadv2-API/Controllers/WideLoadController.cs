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
    public class WideLoadController : ControllerBase
    {
        private const string V = "000000";
        private readonly Kenloadv2Data _context;
        
        public WideLoadController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/WideLoad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WideLoad>>> GetWideLoad( int offset, int limit = 50)
        {
            return await _context.WideLoad.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync(); ;
        }

        // GET: api/WideLoad/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<WideLoad>> GetWideLoad(int id)
        {
            var WideLoad = await _context.WideLoad.FindAsync(id);

            if (WideLoad == null)
            {
                //return NotFound();
            }

            return WideLoad;
        }
        [HttpGet("max")]
        public String GetWideLoadpermitid(String permitid)
        {
            var ticketno = V;

            if (permitid != null)
            {
                ticketno = _context.WideLoad.Where(x => x.permitid.Substring(0, permitid.Length) == permitid).Count() + "";
            }
           

            if (ticketno == null)
            {
                return "000000";
            }
            return ticketno;
        }
        [HttpGet("{permitno}")]
        public async Task<ActionResult<WideLoad>> GetWideLoad(String permitno)
        {
            var wideload = await _context.WideLoad.Where(x => x.permitno == permitno).FirstOrDefaultAsync();

            if (wideload == null)
            {
                return NotFound();
            }

            return wideload;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<WideLoad>>> GetWideLoadSearch(String permitno,  String status, String station, DateTime fromdate, DateTime todate, int offset, int limit = 50,int deletestatus =-1)
        {
            IQueryable<WideLoad> query = _context.WideLoad;
            var predicate = PredicateBuilder.False<WideLoad>();

            //if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && permitno != null)
            //{
            //    var Selections = await _context.WideLoad.Where(x => x.permitdatetime >= fromdate && x.permitdatetime <= todate && x.permitno.Substring(0, permitno.Length) == permitno).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //    return Selections;
            DateTime olddate = DateTime.Parse("01/Jan/0001 12:00:00 AM");
            //}
            if (fromdate != olddate && todate != olddate)
            {
                //var Selections = await _context.WideLoad.Where(x => x.permitdatetime>= fromdate && x.permitdatetime <= todate).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.permitdatetime >= fromdate && x.permitdatetime <= todate);
            }
           
            if (permitno != null)
            {
                if (permitno.Contains(","))
                {
                    var sval = permitno.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.permitno == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.permitno == permitno);
                }



            }
            if (status != null)
            {
                if (status.Contains(","))
                {
                    var sval = status.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.status == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.status == status);
                }
            }

            if (station != null)
            {
                if (station.Contains(","))
                {
                    var sval = station.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
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

            if (deletestatus > -1)
            {
                query = query.Where(x => x.deletestatus == deletestatus);
            }

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        // PUT: api/WideLoad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWideLoad(int id, WideLoad WideLoad)
        {
            if (id != WideLoad.id)
            {
                return BadRequest();
            }

            _context.Entry(WideLoad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WideLoadExists(id))
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

        // POST: api/WideLoad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WideLoad>> PostWideLoad(WideLoad WideLoad)
        {
            _context.WideLoad.Add(WideLoad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWideLoad", new { id = WideLoad.id }, WideLoad);
        }

        // DELETE: api/WideLoad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWideLoad(int id)
        {
            var WideLoad = await _context.WideLoad.FindAsync(id);
            if (WideLoad == null)
            {
                return NotFound();
            }

            _context.WideLoad.Remove(WideLoad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WideLoadExists(int id)
        {
            return _context.WideLoad.Any(e => e.id == id);
        }
    }
}
