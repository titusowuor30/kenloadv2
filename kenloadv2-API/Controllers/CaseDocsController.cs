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
    public class CaseDocsController : ControllerBase
    {
        private const string V = "000000";
        private readonly Kenloadv2Data _context;



        public CaseDocsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/CaseDocs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseDocs>>> GetCaseDocs()
        {
            return await _context.CaseDocs.ToListAsync();
        }

        // GET: api/CaseDocs/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CaseDocs>> GetCaseDocs(int id)
        {
            var CaseDocs = await _context.CaseDocs.FindAsync(id);

            if (CaseDocs == null)
            {
                return NotFound();
            }

            return CaseDocs;
        }
        // GET: api/CaseDocs/records?
        [HttpGet("records")]
        public async Task<ActionResult<IEnumerable<CaseDocs>>> GetCaseDocsRecords(String casedocid, String caseid,String cancelled, String docname, int id, int offset, int limit = 50)
        {

            IQueryable<CaseDocs> query = _context.CaseDocs;
            var predicate = PredicateBuilder.False<CaseDocs>();
            if (docname != null)
            {
                query = query.Where(x => x.docname == docname);
                
            }
         
            if (cancelled != null)
            {
                query = query.Where(x => x.cancelled == cancelled);
               
            }
            if (casedocid != null)
            {
                query = query.Where(x => x.casedocid.Substring(0, casedocid.Length) == casedocid);

            }
            if (caseid != null)
            {
                query = query.Where(x => x.caseid.Substring(0, caseid.Length) == caseid);
                
            }

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CaseDocs>>> GetCaseDocsSearch(String casedocid, String caseid, String docname, int id, int offset, int limit = 50)
        {
            if (docname != null)
            {
                var Selections = await _context.CaseDocs.Where(x => x.docname == docname).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (casedocid != null && caseid != null)
            {
                var Selections = await _context.CaseDocs.Where(x => x.casedocid.Substring(0, casedocid.Length) == casedocid && x.caseid.Substring(0, caseid.Length) == caseid && x.cancelled == "N").OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (casedocid != null)
            {
                var Selections = await _context.CaseDocs.Where(x => x.casedocid.Substring(0, casedocid.Length) == casedocid && x.cancelled=="N").OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (caseid != null)
            {
                var Selections = await _context.CaseDocs.Where(x => x.caseid.Substring(0, caseid.Length) == caseid).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
           
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await _context.CaseDocs.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        [HttpGet("max")]
        public String GetCaseDocsno(String casedocid)
        {
            //_context.CaseDocs.Where(x => x.casedocid.Substring(0, casedocid.Length) == casedocid).ToString();

            if (casedocid != null)
            {
                //System.Linq.Expressions.Expression<Func<CaseDocs, string>> selector = x => x.casedocid.Substring(casedocid.Length, casedocid.Length + 6);
                var ticketnos = _context.CaseDocs.Where(x => x.casedocid.Substring(0, casedocid.Length) == casedocid).Max(k => k.casedocid.Substring(casedocid.Length, casedocid.Length + 6)) + "";
                //ticketno = _context.CaseDetails.Where(x => x.caseid.Substring(0, caseid.Length) == caseid).Max(x => x.caseid.Substring(caseid.Length, caseid.Length + 6));
                //ticketno = _context.CaseDocs.Where(x => x.casedocid.Substring(0, casedocid.Length) != casedocid).ToString();
                if (ticketnos == null)
                {
                    return "000000";
                }
                return ticketnos;

            }
            var ticketno = V;

            if (ticketno == null)
            {
                return "000000";
            }
            return ticketno;
        }
        // PUT: api/CaseDocs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaseDocs(int id, CaseDocs CaseDocs)
        {
            if (id != CaseDocs.id)
            {
                return BadRequest();
            }

            _context.Entry(CaseDocs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseDocsExists(id))
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

        // POST: api/CaseDocs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CaseDocs>> PostCaseDocs(CaseDocs CaseDocs)
        {
            _context.CaseDocs.Add(CaseDocs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaseDocs", new { id = CaseDocs.id }, CaseDocs);
        }

        // DELETE: api/CaseDocs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaseDocs(int id)
        {
            var CaseDocs = await _context.CaseDocs.FindAsync(id);
            if (CaseDocs == null)
            {
                return NotFound();
            }

            _context.CaseDocs.Remove(CaseDocs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CaseDocsExists(int id)
        {
            return _context.CaseDocs.Any(e => e.id == id);
        }
    }
}
