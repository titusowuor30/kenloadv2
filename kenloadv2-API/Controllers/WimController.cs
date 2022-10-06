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
    public class WimController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public WimController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Wim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wim>>> GetWim(int offset, int limit = 50)
        {
            return await _context.Wim.Skip(offset).Take(limit).OrderByDescending(a => a.cid).ToListAsync();
        }

        // GET: api/Wim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wim>> GetWim(int id)
        {
            var Wim = await _context.Wim.FindAsync(id);

            if (Wim == null)
            {
                return NotFound();
            }

            return Wim;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Wim>>> GetWimSearch(String veh_reg, DateTime fromdate, DateTime todate, String cid,int offset, int limit = 50, int calledin=-1)
        {
            IQueryable<Wim> query = _context.Wim;
            var predicate = PredicateBuilder.False<Wim>();

           

            if (calledin >= 0)
            {
                query = query.Where(x => x.calledin == calledin);
            }




            if (veh_reg != null)
            {
                if (veh_reg.Contains(","))
                {
                    var sval = veh_reg.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.lp.Substring(0, val.Length) == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.lp.Substring(0, veh_reg.Length) == veh_reg);
                }
            }
            if (cid != null)
            {
                if (cid.Contains(","))
                {
                    var sval = cid.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        int val = int.Parse(sval[id].Trim());
                        predicate = predicate.Or(x => x.cid == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.cid == int.Parse(cid.Trim()));
                }

            }

            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                query = query.Where(x => x.datetime >= fromdate && x.datetime <= todate);
            }


            //if (wbrg_ticket_no != null || fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") || veh_reg != null || Status != null || transporter != null || cargo != null || caseid != null)
            //{
            //    return await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //}
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }


        // PUT: api/Wim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWim(int id, Wim Wim)
        {
            if (id != Wim.id)
            {
                return BadRequest();
            }

            _context.Entry(Wim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WimExists(id))
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

        // POST: api/Wim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wim>> PostWim(Wim Wim)
        {
            _context.Wim.Add(Wim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWim", new { id = Wim.id }, Wim);
        }

        // DELETE: api/Wim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWim(int id)
        {
            var Wim = await _context.Wim.FindAsync(id);
            if (Wim == null)
            {
                return NotFound();
            }

            _context.Wim.Remove(Wim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WimExists(int id)
        {
            return _context.Wim.Any(e => e.id == id);
        }
      
    }
}
