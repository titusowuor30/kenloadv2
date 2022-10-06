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
    public class ShiftsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public ShiftsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shifts>>> GetShifts()
        {
            return await _context.Shifts.ToListAsync();
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shifts>> GetShifts(int id)
        {
            var shifts = await _context.Shifts.FindAsync(id);

            if (shifts == null)
            {
                return NotFound();
            }

            return shifts;
        }
        // GET: api/Shifts/5

        [HttpGet("by/{email}")]
        public Array Shifts_by_email(string email, int limit = 50)
        {
            var query = (from a in _context.Set<Shifts>().Where(a => a.Email == email) select new { a }).Distinct();
            var Selection = query.Take(limit).ToList();
            return Selection.ToArray();
        }
        //GET : api/Shifts/role/from/to
        [HttpGet("{role}/{fromdate}/{todate}/{shift_type}")]
        public Array ShiftsFilter(String role, String shift_type, DateTime fromdate, DateTime todate)
        {
            IQueryable<Shifts> query = _context.Shifts;
            var predicate = PredicateBuilder.False<Shifts>();


            if (role != null && shift_type != null)
            {
                //query for roles
                if (role.Contains(","))
                {
                    var sval = role.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.position == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else if (role.Contains("All"))
                {
                    //pass
                }
                else
                {
                    query = query.Where(x => x.position == role);
                }
                //query for shift type
                if (shift_type.Contains(","))
                {
                    var sval1 = shift_type.Split(",");

                    int clen1 = sval1.Length;
                    int id1 = 0;
                    for (int g1 = 0; g1 < clen1; g1++)
                    {
                        String val1 = sval1[id1];
                        predicate = predicate.Or(x => x.shift_type == val1);
                        id1++;
                    }

                    query = query.Where(predicate);
                }
                else if (shift_type.Contains("All"))
                {
                    //pass
                }
                else
                {
                    query = query.Where(x => x.shift_type == shift_type);
                }
            }
            var resultsData = query.Where(y => y.From >= fromdate && y.To <= todate).ToList();
            return resultsData.ToArray();
        }

        // PUT: api/Shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShifts(int id, Shifts shifts)
        {
            if (id != shifts.id)
            {
                return BadRequest();
            }

            _context.Entry(shifts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftsExists(id))
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

        // POST: api/Shifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shifts>> PostShifts(Shifts shifts)
        {
            _context.Shifts.Add(shifts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShifts", new { id = shifts.id }, shifts);
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShifts(int id)
        {
            var shifts = await _context.Shifts.FindAsync(id);
            if (shifts == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shifts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftsExists(int id)
        {
            return _context.Shifts.Any(e => e.id == id);
        }
    }
}
