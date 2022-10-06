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
    class specialreleasedoc
    {
        public int id { get; set; }

        public string specialid { get; set; }

        public string weighbridge { get; set; }
        public string driver { get; set; }
        public string transporter { get; set; }

        public string transporteradd { get; set; }
        public string cargo { get; set; }
        public string prohibitionno { get; set; }

        public double excessload { get; set; }

        public DateTime specialreleasedate { get; set; }
        public string regNo { get; set; }

        public string reason { get; set; }
        public string description { get; set; }
        public DateTime weighdate { get; set; }

        public DateTime realeasedate { get; set; }

        public string ticketno { get; set; }
        public string createdby { get; set; }
        public string weighedby { get; set; }

        public DateTime dateProhibited { get; set; }

    }
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SpecialReleaseController : ControllerBase
    {
        private const string V = "000000";
        private readonly Kenloadv2Data _context;

        public SpecialReleaseController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/SpecialRelease
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialRelease>>> GetSpecialRelease()
        {
            return await _context.SpecialRelease.ToListAsync();
        }

        // GET: api/SpecialRelease/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialRelease>> GetSpecialRelease(int id)
        {
            var specialRelease = await _context.SpecialRelease.FindAsync(id);

            if (specialRelease == null)
            {
                return NotFound();
            }

            return specialRelease;
        }
        [HttpGet("max")]
        public String GetMaxno(String specialid)
        {
            var ticketno = V;

            if (specialid != null)
            {
                ticketno = _context.SpecialRelease.Where(x => x.specialid.Substring(0, specialid.Length) == specialid).Max(x => x.specialid.Substring(specialid.Length, specialid.Length + 6));
            }


            if (ticketno == null)
            {
                return "000000";
            }
            return ticketno;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SpecialRelease>>> GetSpecialReleaseSearch(String vehreg, String wbtno, String specialid, DateTime fromdate, DateTime todate,  int offset, int limit = 50)
        {
            IQueryable<SpecialRelease> query = _context.SpecialRelease;

            if (vehreg != null)
            {
                query = query.Where(x => x.vehiclereg== vehreg);
              
            }
            if (wbtno != null)
            {
                query = query.Where(x => x.wbrg_ticket_no.Substring(0, wbtno.Length) == wbtno);
                
            }
            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                query = query.Where(x => x.realeasedate >= fromdate && x.realeasedate <= todate);
            }
            if (specialid != null)
            {
                query = query.Where(x => x.specialid == specialid);
            }
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();

            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        [HttpGet("reports")]
        public Array GetSpecialReleasereports(String vehreg, String wbtno, String specialid, DateTime fromdate, DateTime todate, int offset, int limit = 50)
        {
            IQueryable<SpecialRelease> query = _context.SpecialRelease;
            List<specialreleasedoc> captureRow = new List<specialreleasedoc>();

            if (vehreg != null)
            {
                query = query.Where(x => x.vehiclereg == vehreg);

            }
            if (wbtno != null)
            {
                query = query.Where(x => x.wbrg_ticket_no.Substring(0, wbtno.Length) == wbtno);

            }
            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                query = query.Where(x => x.realeasedate >= fromdate && x.realeasedate <= todate);
            }
            if (specialid != null)
            {
                query = query.Where(x => x.specialid == specialid);
            }
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection2 =query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToArray();
            int rowid = 1;
            for (int g = 0; g < Selection2.Length; g++)     
            {
                IQueryable<WeighbridgeTransactions> queryW = _context.WeighbridgeTransactions.Where(a => a.wbrg_ticket_no == Selection2[g].wbrg_ticket_no);
                var WeighbridgeT = queryW.ToArray();

                var weighedby = WeighbridgeT[0].wbrg_ticket_operator;
                var driver = WeighbridgeT[0].wbrg_ticket_driver;
                var transporter = WeighbridgeT[0].wbrg_tx_code;
                var transporteradd = "";
                var product = WeighbridgeT[0].wbrg_product;
                try
                {
                    weighedby = _context.Users.Where(y => y.email == WeighbridgeT[0].wbrg_ticket_operator).FirstOrDefault().fullnames;
                }
                catch (Exception)
                { }
                IQueryable<ClusterWeighbridge> station = _context.ClusterWeighbridges.Where(a => a.cluster_code == WeighbridgeT[0].wbrg_station);
                var stationT = station.ToArray();
               
               
               


                    captureRow.Add(
                        new specialreleasedoc
                        {
                            id = rowid,
                            specialid = Selection2[g].specialid,
                            weighbridge = stationT[0].cluster_name,
                            driver = driver,
                            transporter = transporter,
                            transporteradd = transporteradd,
                            cargo = product,
                            specialreleasedate = Selection2[g].realeasedate,
                            regNo = Selection2[g].vehiclereg,
                            reason = Selection2[g].reason,
                            description = Selection2[g].otherreason,
                            weighdate = WeighbridgeT[0].wbrg_ticket_date,
                            ticketno = WeighbridgeT[0].wbrg_ticket_no,
                            createdby = Selection2[g].realeasedby,
                            weighedby = weighedby,
                            realeasedate= Selection2[g].realeasedate,
                            excessload = Selection2[g].excessload,

                            //ProhibitedNo = row.q.casedocid,
                            //dateProhibited=row.q.docdate.ToString(),
                            //SpecialReleaseNo= row.a.specialid,
                            //realeasedate = row.a.realeasedate.ToString(),
                            //spreason = row.a.reason,
                            //spotherreason = row.a.otherreason,
                            //releasedby = row.a.realeasedby,
                        }
                        );
                rowid++;
                
            }
            

            var Selection =  captureRow.ToArray();

           
            return Selection;

        }
        [HttpGet("report")]
        public Array GetSpecialReleaseReport(String vehreg, String wbtno, String specialid, DateTime fromdate, DateTime todate, int offset, int limit = 50)
        {
            IQueryable<SpecialRelease> query = _context.SpecialRelease;

           

            if (specialid != null)
            {
                query = query.Where(x => x.specialid == specialid);
            }
            if (vehreg != null)
            {
                query = query.Where(x => x.vehiclereg == vehreg);

            }
            if (wbtno != null)
            {
                query = query.Where(x => x.wbrg_ticket_no.Substring(0, wbtno.Length) == wbtno);

            }
           
           
            var Selection = query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToArray();


            IQueryable<WeighbridgeTransactions> queryW = _context.WeighbridgeTransactions.Where(a => a.wbrg_ticket_no == Selection[0].wbrg_ticket_no);
            var WeighbridgeT = queryW.ToArray();

            IQueryable<CaseDetails> CaseDetails = _context.CaseDetails.Where(a => a.caseticket == Selection[0].wbrg_ticket_no).Include(CaseDocs => CaseDocs.casedocs.Where(b => b.cancelled=="N"));
            var CaseDetailsT = CaseDetails.ToArray();

            IQueryable<ClusterWeighbridge> station = _context.ClusterWeighbridges.Where(a => a.cluster_code == WeighbridgeT[0].wbrg_station);
            var stationT = station.ToArray();

            var prohibitionno = "";
            var driver = WeighbridgeT[0].wbrg_ticket_driver;
            var transporter = WeighbridgeT[0].wbrg_tx_code;
            var transporteradd = "";
            var product = WeighbridgeT[0].wbrg_product;
            var dateProhibited=new DateTime();
            if (CaseDetailsT.Length > 0)
            {
                transporteradd = CaseDetailsT[0].transporteradd;
                transporter = CaseDetailsT[0].transportername;
                driver = CaseDetailsT[0].drivername;
                var caseticket4 = CaseDetailsT[0].casedocs.ToArray();
                prohibitionno = caseticket4[0].casedocid;
                dateProhibited= caseticket4[0].docdate;
            }
            var weighedby = WeighbridgeT[0].wbrg_ticket_operator;
            try
            {
                weighedby = _context.Users.Where(y => y.email == WeighbridgeT[0].wbrg_ticket_operator).FirstOrDefault().fullnames;
            }
            catch (Exception)
            { }
            List<specialreleasedoc> captureRow = new List<specialreleasedoc>();
            int rowid = 0;
            foreach (var row in query)
            {


                captureRow.Add(
                    new specialreleasedoc
                    {
                        id = rowid,
                        specialid = Selection[0].specialid,
                        weighbridge = stationT[0].cluster_name,
                        driver = driver,
                        transporter = transporter,
                        prohibitionno = prohibitionno,
                        transporteradd = transporteradd,
                        cargo=product,
                        specialreleasedate = Selection[0].realeasedate,
                        regNo = Selection[0].vehiclereg,
                        reason = Selection[0].reason,
                        description = Selection[0].otherreason,
                        weighdate = WeighbridgeT[0].wbrg_ticket_date,
                        ticketno = WeighbridgeT[0].wbrg_ticket_no,
                        createdby = Selection[0].realeasedby,
                        weighedby = weighedby,
                        dateProhibited = dateProhibited,
                        excessload = Selection[0].excessload,
                        //ProhibitedNo = row.q.casedocid,
                        //dateProhibited=row.q.docdate.ToString(),
                        //SpecialReleaseNo= row.a.specialid,
                        //realeasedate = row.a.realeasedate.ToString(),
                        //spreason = row.a.reason,
                        //spotherreason = row.a.otherreason,
                        //releasedby = row.a.realeasedby,
                    }
                    ); 
                rowid++;
            }

            var Selection2 = captureRow.ToArray();


            return Selection2; ;

        }
        // PUT: api/SpecialRelease/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialRelease(int id, SpecialRelease specialRelease)
        {
            if (id != specialRelease.id)
            {
                return BadRequest();
            }

            _context.Entry(specialRelease).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialReleaseExists(id))
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

        // POST: api/SpecialRelease
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpecialRelease>> PostSpecialRelease(SpecialRelease specialRelease)
        {
            _context.SpecialRelease.Add(specialRelease);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialRelease", new { id = specialRelease.id }, specialRelease);
        }

        // DELETE: api/SpecialRelease/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialRelease(int id)
        {
            var specialRelease = await _context.SpecialRelease.FindAsync(id);
            if (specialRelease == null)
            {
                return NotFound();
            }

            _context.SpecialRelease.Remove(specialRelease);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialReleaseExists(int id)
        {
            return _context.SpecialRelease.Any(e => e.id == id);
        }

    }
}
