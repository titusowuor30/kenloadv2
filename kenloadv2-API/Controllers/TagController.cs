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
    public class TagController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public TagController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTag(int offset, int limit = 50)
        {
            return await _context.Tag.Skip(offset).Take(limit).ToListAsync();
        }

        // GET: api/Tag/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var Tag = await _context.Tag.FindAsync(id);

            if (Tag == null)
            {
                return NotFound();
            }

            return Tag;
        }
     

        [HttpGet("{vehreg}")]
        public async Task<ActionResult<Tag>> GetTag(String vehreg)
        {
            var Tag = await _context.Tag.Where(x => x.vehreg == vehreg).FirstOrDefaultAsync();

            if (Tag == null)
            {
                //return NotFound();
            }

            return Tag;
        }

        [HttpGet("station")]
        public async Task<ActionResult<Tag>> GetTagStation(String station)
        {
            var Tag = await _context.Tag.Where(x => x.station == station).FirstOrDefaultAsync();

            if (Tag == null)
            {
                //return NotFound();
            }

            return Tag;
        }
        [HttpGet("taguid")]
        public async Task<ActionResult<Tag>> GetTagGuid(String taguid)
        {
            var Tag = await _context.Tag.Where(x => x.taguid== taguid).FirstOrDefaultAsync();

            if (Tag == null)
            {
                //return NotFound();
            }

            return Tag;
        }
        [HttpGet("getOtherNewTags")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagOtherTags(int offset, int limit = 50, string exported = "")
        {
            IQueryable<Tag> query = _context.Tag;
            query = query.Where(x => !x.exported.Contains(exported)).Where(x => x.exported.Substring(0, 1).Contains("N"));
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;
        }
        [HttpGet("getModifiedTags")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetModifiedTags(int offset, int limit = 50, string exported ="")
        {
            IQueryable<Tag> query = _context.Tag;
            query = query.Where(x => !x.exported.Contains(exported)).Where(x => x.exported.Substring(0, 1).Contains("M"));
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagSearch(String vehreg, String station, String exported, String user,String type, DateTime fromdate, DateTime todate, int offset, int limit = 50, int status = -1)
        {

            IQueryable<Tag> query = _context.Tag;
            var predicate = PredicateBuilder.False<Tag>();


            if (vehreg != null)
            {
                query = query.Where(x => x.vehreg.Trim() == vehreg.Trim());
               
            }
            if (type != null)
            {

                if (type.Contains(","))
                {
                    var rou = type.Split(",");

                    int clen = rou.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = rou[id];
                        predicate = predicate.Or(x => x.type == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.type == type);
                }

            }
            if (user != null)
            {

                if (user.Contains(","))
                {
                    var rou = user.Split(",");

                    int clen = rou.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = rou[id];
                        try
                        {
                            predicate = predicate.Or(x => x.userid.Contains(val));
                        }
                        catch (Exception) { 
                        }
                        
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.userid.Contains(user));
                }

            }
            if (exported != null)
            {
                //query = query.Where(x =>!x.exported.Contains(exported)).Where(x=>x.exported.Substring(0,1)=="N");
                query = query.Where(x => x.exported == exported);
            }
            if ((fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM")) )
            {
                query = query.Where(x => x.datetime >= fromdate && x.datetime <= todate);
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
            if (status != -1)
            {
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
        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> PutTag(int id, Tag tag)
        {
            var stationName = _context.SystemSettings.FirstOrDefault().stationcode;
            if (id != tag.id)
            {
                return BadRequest();
            }
           // tag.exported = "M," + stationName;
            _context.Entry(tag).State = EntityState.Modified;
             try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
           
            return tag;
        }

        [HttpPut("taguid")]
        public async Task<ActionResult<Tag>> PutTagGuid(Tag tag)//update tag with tag ID
        {
            try
            {

              
                var tagsUpdate = _context.Tag.Where(x => x.taguid == tag.taguid);
                
                //_context.Entry(tag).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
 
                e.ToString();
                _context.Tag.Add(tag);
                await _context.SaveChangesAsync();
            }
            return tag;
        }

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            //get station
            // ||
            var stationName = _context.SystemSettings.FirstOrDefault();
            try
            {
                var lastTagId = _context.Tag.OrderByDescending(p => p.id).FirstOrDefault().id;
                if (string.IsNullOrEmpty(tag.taguid) || tag.taguid.Length < 10)
                {
                    DateTime newDate = DateTime.Now;
                    var tagIdGen = "TAG" + stationName.stationcode + newDate.Date.Year + newDate.Date.Month + (lastTagId + 1);
                    tag.taguid = tagIdGen;
                    
                }
            }
            catch(Exception ex)
            {
                DateTime newDate = DateTime.Now;
                var tagIdGen = "TAG" + stationName.stationcode + newDate.Date.Year + newDate.Date.Month + 1;
                tag.taguid = tagIdGen;
                ex.ToString();

            }
           // tag.exported = "N,"+stationName.stationcode;
           _context.Tag.Add(tag);   
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTag", new { id = tag.id }, tag); 

        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            /*var Tag = await _context.Tag.FindAsync(id);
            if (Tag == null)
            {
                return NotFound();
            }

            _context.Tag.Remove(Tag);
            await _context.SaveChangesAsync();*/

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tag.Any(e => e.id == id);
        }

    }
}
