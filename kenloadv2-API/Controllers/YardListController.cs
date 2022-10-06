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
    public class YardListController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public YardListController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/YardList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YardList>>> GetYardList()
        {
            return await _context.YardList.ToListAsync();
        }

        // GET: api/YardList/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<YardList>> GetYardList(int id)
        {
            var YardList = await _context.YardList.FindAsync(id);

            if (YardList == null)
            {
                return NotFound();
            }

            return YardList;
        }

        [HttpGet("{vehreg}")]
        public async Task<ActionResult<IEnumerable<YardList>>> GetYardList(String vehreg)
        {
            var ylist = await _context.YardList.Where(x => x.vehreg == vehreg).ToListAsync();

            if (ylist == null)
            {
                return NotFound();
            }

            return ylist;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<YardList>>> GetYardListSearch(String vehreg,  String wbtno, String spreleaseno, String station, String status, String caseid, int offset, int limit = 50, int inspected=-1)
        {

            IQueryable<YardList> query = _context.YardList;
            var predicate = PredicateBuilder.False<YardList>();
           

            //if (status != null && vehreg != null && caseid != null)
            //{
            //    var Selections = await _context.YardList.Where(x => x.vehreg == vehreg && x.status == status && x.caseid.Substring(0,caseid.Length) != caseid).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //    return Selections;

            //}
            //if (status != null && vehreg != null)
            //{
            //    var Selections = await _context.YardList.Where(x => x.vehreg == vehreg && x.status == status).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //    return Selections;
            //}

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
            if (inspected >=0)
            {
                //var Selections = await _context.YardList.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.inspected== inspected);
            }
            if (spreleaseno != null)
            {
                //var Selections = await _context.YardList.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.spreleaseno == spreleaseno);
            }
            if (caseid != null)
            {
                //var Selections = await _context.YardList.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.caseid.Substring(0, caseid.Length) != caseid);
            }
            if (vehreg != null)
            {
                //var Selections = await _context.YardList.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.vehreg == vehreg);
            }
            if (wbtno != null)
            {
                //var Selections = await _context.YardList.Where(x => x.wbtno.Substring(0, wbtno.Length) == wbtno).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.wbtno.Substring(0, wbtno.Length) == wbtno);
            }
            if (status != null )
            {
                //var Selections = await _context.YardList.Where(x => x.status == status).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.status == status);
            }
           
                //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
                var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        // PUT: api/YardList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYardList(int id, YardList YardList)
        {
            if (id != YardList.id)
            {
                return BadRequest();
            }
            //var updateyardlist = _context.YardList.FirstOrDefault(c => c.id == id);

            //updateyardlist.vehreg = YardList.vehreg;
            //updateyardlist. = YardList.vehreg;
            _context.Entry(YardList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YardListExists(id))
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
        [HttpPut("inspected")]
        public async Task<IActionResult> PutinspectedYardList(int id, YardList YardList)
        {
            if (id != YardList.id)
            {
                return BadRequest();
            }
            var updateyardlist = _context.YardList.FirstOrDefault(c => c.id == id);

            updateyardlist.inspected = YardList.inspected;
            _context.Entry(updateyardlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YardListExists(id))
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

        // POST: api/YardList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<YardList>> PostYardList(YardList YardList)
        {
            _context.YardList.Add(YardList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYardList", new { id = YardList.id }, YardList);
        }

        // DELETE: api/YardList/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteYardList(int id)
        //{
        //    var YardList = await _context.YardList.FindAsync(id);
        //    if (YardList == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.YardList.Remove(YardList);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool YardListExists(int id)
        {
            return _context.YardList.Any(e => e.id == id);
        }
    }
}
