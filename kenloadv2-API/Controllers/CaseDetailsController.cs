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
     class improbData
    {
        public int id { get; set; }
        public String station { get; set; }
        public String dateWeighed { get; set; }
        public String dateProhibited { get; set; }
        public String timeDiff { get; set; }
        public String ticketNO { get; set; }
        public String transporter { get; set; }
        public String regNo { get; set; }
        public String axleConfig { get; set; }
        public String cargo { get; set; }
        public String source { get; set; }
        public String destination { get; set; }
        public String axleoverload { get; set; }
        public String gvwaxleload { get; set; }
        public String status { get; set; }
        public String prohibitionorder { get; set; }
        public String prosecutor { get; set; }
        public String computerOperator { get; set; }
    }

    class improbDataOverload
    {
        public int id { get; set; }

        public String station { get; set; }
        public DateTime dateWeighed { get; set; }
        public DateTime lastdateWeighed { get; set; }
        public String dateProhibited { get; set; }
        public String realeasedate { get; set; }
        public String ProhibitedNo { get; set; }
        public String timeDiff { get; set; }
        public String ticketNO { get; set; }
        public String transporter { get; set; }
        public String regNo { get; set; }
        public String axleConfig { get; set; }
        public String lastaxleConfig { get; set; }
        public String cargo { get; set; }
        public String source { get; set; }
        public String destination { get; set; }
        public String axleoverload { get; set; }
        public String lastaxleoverload { get; set; }
        public String gvwaxleload { get; set; }

        public String lastgvwaxleload { get; set; }
        public String status { get; set; }
        public String prohibitionorder { get; set; }
        public String prosecutor { get; set; }
        public String computerOperator { get; set; }
        public String reweighno { get; set; }

        public String lastcomputerOperator { get; set; }
        public String SpecialReleaseNo { get; set; }


        public String spreason { get; set; }
        public String spotherreason { get; set; }
        public String releasedby { get; set; }
        public String state { get; set; }
        public String laststate { get; set; }
        public String vardict { get; set; }
    }


    [EnableCors("ServerPolicy")]
        [Route("api/[controller]")]
        [ApiController]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CaseDetailsController : ControllerBase
    {
        private const string V = "000000";
        private readonly Kenloadv2Data _context;

        public CaseDetailsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/CaseDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseDetails>>> GetCaseDetails(int offset, int limit = 50)
        {
            
            return await _context.CaseDetails.Include(CaseDocs => CaseDocs.casedocs).Include(CaseResult => CaseResult.caseresults).Include(BillingInstruction => BillingInstruction.billinginstruction).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
        }

        // GET: api/CaseDetails/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CaseDetails>> GetCaseDetails(int id)
        {
            var CaseDetails = await _context.CaseDetails.FindAsync(id);
            //var CaseDetails = await _context.CaseDetails.Include(CaseDocs => CaseDocs.casedocs).Include(CaseResult => CaseResult.caseresults).Include(BillingInstruction => BillingInstruction.billinginstruction).Where(x => x.id == id).OrderByDescending(a => a.id).ToListAsync();



            if (CaseDetails == null)
            {
                return NotFound();
            }

            return CaseDetails;
        }

        [HttpGet("{vehreg}")]
        public async Task<ActionResult<IEnumerable<CaseDetails>>> GetCaseDetails(String vehreg)
        {
            var ylist = await _context.CaseDetails.Include(CaseDocs => CaseDocs.casedocs).Include(CaseResult => CaseResult.caseresults).Include(BillingInstruction => BillingInstruction.billinginstruction).Where(x => x.caseregno == vehreg).OrderByDescending(a => a.id).ToListAsync();

            if (ylist == null)
            {
                return NotFound();
            }

            return ylist;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CaseDetails>>> GetCaseDetailsSearch(String vehreg, String wbtno, String caseid, String docs,  int offset, int limit = 50)
        {
            //IQueryable<Wim> query = (IQueryable<Wim>)_context.CaseDetails.Include(CaseDocs => CaseDocs.casedocs).Include(CaseResult => CaseResult.caseresults).Include(BillingInstruction => BillingInstruction.billinginstruction);

            if (docs != null)
            {
                //query = query.Where(x => x.weighbridgetransactionsid == wbtno);
                var Selections = await _context.CaseDetails.Include(CaseDocs => CaseDocs.casedocs.Where(b => b.docname == docs)).Include(CaseResult => CaseResult.caseresults).Include(BillingInstruction => BillingInstruction.billinginstruction).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (caseid != null)
            {
                 var Selections = await _context.CaseDetails.Where(x => x.caseid.Substring(0, caseid.Length) == caseid).Include(CaseDocs => CaseDocs.casedocs).Include(CaseResult => CaseResult.caseresults).Include(BillingInstruction => BillingInstruction.billinginstruction).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (wbtno != null)
            {
                var Selections = await _context.CaseDetails.Include(CaseDocs => CaseDocs.casedocs).Include(CaseResult => CaseResult.caseresults).Include(BillingInstruction => BillingInstruction.billinginstruction).Where(x => x.caseticket.Substring(0, wbtno.Length) == wbtno).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }

            if (vehreg != null)
            {
                var Selections = await _context.CaseDetails.Where(x => x.caseregno.Substring(0, vehreg.Length) == vehreg).Include(CaseDocs => CaseDocs.casedocs).Include(CaseResult => CaseResult.caseresults).Include(BillingInstruction => BillingInstruction.billinginstruction).OrderByDescending(a => a.casedatetime).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await _context.CaseDetails.Include(CaseDocs => CaseDocs.casedocs).Include(CaseResult => CaseResult.caseresults).Include(BillingInstruction => BillingInstruction.billinginstruction).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        [HttpGet("max")]
        public String GetCaseDetailsWBTno(String caseid)
        {
            var ticketno = V;

            if (caseid != null)
            {
                ticketno = _context.CaseDetails.Where(x => x.caseid.Substring(0, caseid.Length) == caseid).Max(x => x.caseid.Substring(caseid.Length, caseid.Length + 6));
            }


            if (ticketno == null)
            {
                return "000000";
            }
            return ticketno;
        }

        // PUT: api/CaseDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaseDetails(int id, CaseDetails CaseDetails)
        {
            if (id != CaseDetails.id)
            {
                return BadRequest();
            }

            _context.Entry(CaseDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseDetailsExists(id))
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

        // POST: api/CaseDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CaseDetails>> PostCaseDetails(CaseDetails CaseDetails)
        {
            _context.CaseDetails.Add(CaseDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaseDetails", new { id = CaseDetails.id }, CaseDetails);
        }

        // DELETE: api/CaseDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaseDetails(int id)
        {
            var CaseDetails = await _context.CaseDetails.FindAsync(id);
            if (CaseDetails == null)
            {
                return NotFound();
            }

            _context.CaseDetails.Remove(CaseDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("report/impoundedAndOverloaded")]

        public Array impoundedAndOverload(DateTime fromdate, DateTime todate)
        {
            int rowid = 1;
            List<improbDataOverload> captureRow = new List<improbDataOverload>();
            IQueryable<YardList> queryw = _context.YardList;
            IQueryable<WeighbridgeTransactions> lastq = _context.WeighbridgeTransactions;
            IQueryable<Users> queryu = _context.Users;

            var query = from w in _context.WeighbridgeTransactions where (w.wbrg_ticket_date >= fromdate && w.wbrg_ticket_date <= todate) && w.wbrg_ticket_status == "O" && w.wbrg_ticket_ref == ""
                        //join k in _context.YardList on w.wbrg_ticket_no equals k.wbtno 
                        //from y in _context.Set<WeighbridgeTransactions>().Where(y => y.wbrg_ticket_ref == w.wbrg_ticket_no.Substring(0, 17)).OrderByDescending(a => a.id).Take(1)
                        //
                        //from z in _context.Set<CaseDetails>().Where(p => p.caseticket.Substring(0, 17) == w.wbtno)
                        //from q in _context.Set<CaseDocs>().Where(q => z.id == q.casedetailsid).Where(q => q.docname == "Prohibition Order").Where(q => z.casedatetime >= fromdate && z.casedatetime <= todate)
                        //from a in _context.Set<SpecialRelease>().Where(a => a.wbrg_ticket_no.Substring(0, 17) == w.wbtno)

                        select new {w};

            //return captureRow.ToArray();
            DateTime to = todate.AddDays(1);
            var Selection = queryw.Where(p => p.datetime >= fromdate && p.datetime <= todate).ToArray();
            var last = lastq.Where(p => (p.wbrg_ticket_date>= fromdate && p.wbrg_ticket_date <= to) && p.wbrg_ticket_ref !="").OrderByDescending(a => a.id).ToArray();
            var users = queryu.ToArray();

            foreach (var row in query)
            {

                int sel2 = -1;
                var tslastdateWeighed = row.w.wbrg_ticket_date;
                var tslastaxleConfig = "";
                var tslastaxleoverload = "";
                var tslastgvwaxleload = "0";
                var tslastcomputerOperator = "";
                var tslaststate = "";
                var tsvardict = row.w.wbrg_ticket_directionarea;
                var lastreweighno = "0";

                var username = row.w.wbrg_ticket_operator;
                
                for (int s = 0; s < users.Length; s++)
                {
                    if (users[s].email == username)
                    {
                        username = users[s].fullnames;
                        s = users.Length + 1;
                    }
                }

                for (int s = 0; s < last.Length; s++)
                {
                    if (last[s].wbrg_ticket_ref == row.w.wbrg_ticket_no)
                    {
                        sel2 = s;
                        s = last.Length + 1;
                    }
                }
                
                String status = "";
                TimeSpan ts = (TimeSpan)(row.w.wbrg_ticket_date - row.w.wbrg_ticket_date);
              
                    tslastaxleoverload = row.w.wbrg_ticket_axleload.ToString();
                    tslastgvwaxleload = row.w.wbrg_ticket_gvwload.ToString();
                
                if (sel2 > -1)
                {
                    ts = (TimeSpan)(last[sel2].wbrg_ticket_date- row.w.wbrg_ticket_date);
                    tslastdateWeighed = last[sel2].wbrg_ticket_date;
                    tslastaxleConfig = last[sel2].wbrg_ticket_axel;
                    tslastaxleoverload = last[sel2].wbrg_ticket_axleload.ToString();
                    tslastgvwaxleload = last[sel2].wbrg_ticket_gvwload.ToString();
                    tslastcomputerOperator = last[sel2].wbrg_ticket_operator;
                    tsvardict = last[sel2].wbrg_ticket_directionarea;
                    lastreweighno = last[sel2].wbrg_ticket_no.ToString().Substring(18);
                    var val = "";
                    if (last[sel2].wbrg_ticket_state == "S")
                    {
                        val = "Special Release";
                    }
                    if (last[sel2].wbrg_ticket_state == "T")
                    {
                        val = "To Redistribute";
                    }
                    if (last[sel2].wbrg_ticket_state == "C")
                    {
                        val = "Charged";
                    }
                    if (last[sel2].wbrg_ticket_state == "R")
                    {
                        val = "Redistributed";
                    }
                    
                    tslaststate = val;
                    for (int s = 0; s < users.Length; s++)
                    {
                        if (users[s].email == tslastcomputerOperator)
                        {
                            tslastcomputerOperator = users[s].fullnames;
                            s = users.Length + 1;
                        }
                    }
                }


                
                int sel = 0;
                for (int s = 0; s < Selection.Length; s++)
                {
                    if (Selection[s].wbtno == row.w.wbrg_ticket_no)
                    {
                        sel = s;
                        s= Selection.Length+1;
                    }
                }
                if (Selection[sel].status == "O") { status = "Released"; }
                else if (Selection[sel].status == "I") { status = "In Yard"; }
                
                //String len = row.y.wbrg_ticket_no.ToString();


                captureRow.Add(
                    new improbDataOverload
                    {
                        id = rowid,
                        dateWeighed = row.w.wbrg_ticket_date,
                        lastdateWeighed = tslastdateWeighed,
                        timeDiff = ts.ToString(),
                        ticketNO = row.w.wbrg_ticket_no,
                        transporter = row.w.wbrg_tx_code,
                        regNo = row.w.veh_reg,
                        axleConfig = row.w.wbrg_ticket_axel,
                        lastaxleConfig = tslastaxleConfig,
                        cargo = row.w.wbrg_product,
                        destination = row.w.wbrg_ticket_destination,
                        source = row.w.wbrg_ticket_source,
                        axleoverload = row.w.wbrg_ticket_axleload.ToString(),
                        gvwaxleload = row.w.wbrg_ticket_gvwload.ToString(),
                        lastaxleoverload = tslastaxleoverload,
                        lastgvwaxleload = tslastgvwaxleload,
                        status = status,
                        computerOperator = username,
                        lastcomputerOperator = tslastcomputerOperator,
                        state = row.w.wbrg_ticket_state=="S" ? "Special Release": row.w.wbrg_ticket_state == "R" ? "Redistributed" : row.w.wbrg_ticket_state == "S" ? "Special Release" : row.w.wbrg_ticket_state == "T" ? "To Redistribute" : row.w.wbrg_ticket_state == "C" ? "Charged" : "",
                        laststate = tslaststate,
                        vardict = tsvardict,
                        reweighno = lastreweighno,
                        //ProhibitedNo = row.q.casedocid,
                        //dateProhibited=row.q.docdate.ToString(),
                        //SpecialReleaseNo= row.a.specialid,
                        //realeasedate = row.a.realeasedate.ToString(),
                        //spreason = row.a.reason,
                        //spotherreason = row.a.otherreason,
                        //releasedby = row.a.realeasedby,
                    }
                    );;
                rowid++;
            }
            return captureRow.ToArray();

        }
        [HttpGet("report/impoundedAndProhibited")]

        public Array impoundedAndProhibited(DateTime fromdate, DateTime todate)
        {
            int rowid = 1;
            List<improbData> captureRow = new List<improbData>();
            IQueryable<Users> queryw = _context.Users;

            var query = from b in _context.Set<CaseDetails>()
                        from p in _context.Set<CaseDocs>().Where(p => b.id == p.casedetailsid).Where(p=>p.docname == "Prohibition Order").Where(p =>b.casedatetime >= fromdate && b.casedatetime <= todate)
                        from w in _context.Set<WeighbridgeTransactions>().Where(w => w.wbrg_ticket_no ==b.caseticket)
                        from y in _context.Set<YardList>().Where(y => y.wbtno == w.wbrg_ticket_no.Substring(0,17))

                        select new { b, p, w ,y };

            var users = queryw.ToArray();

           
            //return captureRow.ToArray();
            foreach  (var row in query)
            {
                
                String status = "";
                TimeSpan ts = (TimeSpan)( row.p.docdate - row.b.weighdate);
                if (row.y.status == "O") { status = "Released"; }
                else if (row.y.status == "I") { status = "In Yard"; }
                var username = row.p.createdby;
                for (int s = 0; s < users.Length; s++)
                {
                    if (users[s].email == row.p.createdby)
                    {
                        username = users[s].fullnames;
                        s = users.Length + 1;
                    }
                }

                var usernameop = row.b.WBT_operator;
                for (int k = 0; k < users.Length; k++)
                {
                    if (users[k].email == row.b.WBT_operator)
                    {
                        usernameop = users[k].fullnames;
                        k = users.Length + 1;
                    }
                }


                captureRow.Add(
                    new improbData
                    {
                        id=rowid,
                        dateWeighed = row.b.weighdate.ToString(),
                        dateProhibited = row.p.docdate.ToString(),
                        timeDiff = ts.ToString(),
                        ticketNO = row.b.caseticket,
                        transporter = row.b.transportername,
                        regNo = row.b.caseregno,
                        axleConfig = row.b.ticketaxletype,
                        cargo = row.w.wbrg_product,
                        destination = row.w.wbrg_ticket_destination,
                        source = row.w.wbrg_ticket_source,
                        axleoverload = row.w.wbrg_ticket_axleload.ToString(),
                        gvwaxleload = row.w.wbrg_ticket_gvwload.ToString(),
                        status = status,
                        prohibitionorder = row.p.casedocid,
                        prosecutor = username,
                        computerOperator = usernameop,

                    }
                    );
                rowid++;
            }
            return captureRow.ToArray();

        }

            private bool CaseDetailsExists(int id)
        {
            return _context.CaseDetails.Any(e => e.id == id);
        }
        
    }
}
