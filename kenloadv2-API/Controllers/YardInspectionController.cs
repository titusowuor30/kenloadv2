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
    public class YardInspectionController : ControllerBase
    {
        private const string V = "000000";
        private readonly Kenloadv2Data _context;
        
        public YardInspectionController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/YardInspection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YardInspection>>> GetYardInspection()
        {
            return await _context.YardInspection.ToListAsync();
        }

        // GET: api/YardInspection/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<YardInspection>> GetYardInspection(int id)
        {
            var YardInspection = await _context.YardInspection.FindAsync(id);

            if (YardInspection == null)
            {
                //return NotFound();
            }

            return YardInspection;
        }
        [HttpGet("max")]
        public String GetYardInspectioninspectionid(String inspectionid)
        {
            var ticketno = V;

            if (inspectionid != null)
            {
                ticketno = _context.YardInspection.Where(x => x.inspectionid.Substring(0, inspectionid.Length) == inspectionid).Count() + "";
            }
           

            if (ticketno == null)
            {
                return "000000";
            }
            return ticketno;
        }
       
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<YardInspection>>> GetYardInspectionSearch(String permitno,  String status, DateTime fromdate, DateTime todate, int offset, int limit = 50,int deletestatus=-1)
        {
            IQueryable<YardInspection> query = _context.YardInspection;

            //if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && permitno != null)
            //{
            //    var Selections = await _context.YardInspection.Where(x => x.permitdatetime >= fromdate && x.permitdatetime <= todate && x.permitno.Substring(0, permitno.Length) == permitno).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //    return Selections;
               
            //}
            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                //var Selections = await _context.YardInspection.Where(x => x.permitdatetime>= fromdate && x.permitdatetime <= todate).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.datetime >= fromdate && x.datetime <= todate);
            }
            
            if (status != null)
            {
                //var Selections = await _context.YardInspection.Where(x => x.status == status).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                query = query.Where(x => x.status == status);
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
        // PUT: api/YardInspection/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYardInspection(int id, YardInspection YardInspection)
        {
            if (id != YardInspection.id)
            {
                return BadRequest();
            }

            _context.Entry(YardInspection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YardInspectionExists(id))
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

        // POST: api/YardInspection
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<YardInspection>> PostYardInspection(YardInspection YardInspection)
        {
            _context.YardInspection.Add(YardInspection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYardInspection", new { id = YardInspection.id }, YardInspection);
        }

        // DELETE: api/YardInspection/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteYardInspection(int id)
        //{
        //    var YardInspection = await _context.YardInspection.FindAsync(id);
        //    if (YardInspection == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.YardInspection.Remove(YardInspection);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool YardInspectionExists(int id)
        {
            return _context.YardInspection.Any(e => e.id == id);
        }
    }
}
