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
  
    public class CaseResultsController : ControllerBase
    {
        private const string V = "000000";
        private readonly Kenloadv2Data _context;

        public CaseResultsController(Kenloadv2Data context)
        {
            _context = context;
        }
        public class courtfineList
        {
            public DateTime? paymentDate { get; set; }
            public string noPlate { get; set; }
            public string transporter { get; set; }
            public string transporterAddress { get; set; }
            public string axle { get; set; }
            public DateTime? dateCharged { get; set; }
            public double excessAxle { get; set; }
            public double excessGVW { get; set; }
            public double driverCharge { get; set; }
            public double ownerCharge { get; set; }
            public double transporterCharge { get; set; }
            public double loaderCharge { get; set; }
            public double parkingCharge { get; set; }
            public double totalCharge { get; set; }
            public string receiptNo { get; set; }
            public string wbtTicketNo { get; set; }
            public string prohibitionOrderNo { get; set; }



        }
        public class processList
        {
            public string wbtTickentNo { get; set; }
            public DateTime? wbtTicketDate { get; set; }
            public string registration { get; set; }
            public string transporter { get; set; }
            public string axel { get; set; }
            public string excess { get; set; }
            public string prohibitionOrder { get; set; }
            public string conditionalOrder { get; set; }
            public string chargeSheet { get; set; }
            public string courtResults { get; set; }
            public string loadCorrection { get; set; }
            public string complyCertificate { get; set; }
            public string specialRelease { get; set; }
            public string totalAmount { get; set; }


        }
        // GET: api/CaseResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseResults>>> GetCaseResults(int offset, int limit = 50)
        {
            
            return await _context.CaseResults.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
        }

        // GET: api/CaseResults/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CaseResults>> GetCaseResults(int id)
        {
            var CaseResults = await _context.CaseResults.FindAsync(id);

            if (CaseResults == null)
            {
                return NotFound();
            }

            return CaseResults;
        }

       
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CaseResults>>> GetCaseResultsSearch(String vehreg, String wbtno, String caseid, int deleted,  int offset, int limit = 50)
        {

            if (caseid != null && deleted == 5)
            {
                var Selections = await _context.CaseResults.Where(x => x.caseid.Substring(0, caseid.Length) == caseid && x.deleted == 0).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (caseid != null)
            {
                 var Selections = await _context.CaseResults.Where(x => x.caseid.Substring(0, caseid.Length) == caseid).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            
            if (vehreg != null)
            {
                var Selections = await _context.CaseResults.Where(x => x.casereregno.Substring(0, vehreg.Length) == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (wbtno != null)
            {
                var Selections = await _context.CaseResults.Where(x => x.caseid.Substring(0, wbtno.Length) == wbtno).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
           
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await _context.CaseResults.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        [HttpGet("max")]
        public String GetCaseResultsWBTno(String casereid)
        {
            var ticketno = V;

            if (casereid != null)
            {
                ticketno = _context.CaseResults.Where(x => x.casereid.Substring(0, casereid.Length) == casereid).Max(x => x.casereid.Substring(casereid.Length, casereid.Length + 6));
            }


            if (ticketno == null)
            {
                return "000000";
            }
            return ticketno;
        }

        // PUT: api/CaseResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaseResults(int id, CaseResults CaseResults)
        {
            if (id != CaseResults.id)
            {
                return BadRequest();
            }

            _context.Entry(CaseResults).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseResultsExists(id))
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

        // POST: api/CaseResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CaseResults>> PostCaseResults(CaseResults CaseResults)
        {
            _context.CaseResults.Add(CaseResults);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaseResults", new { id = CaseResults.id }, CaseResults);
        }

        // DELETE: api/CaseResults/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCaseResults(int id)
        //{
        //    var CaseResults = await _context.CaseResults.FindAsync(id);
        //    if (CaseResults == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CaseResults.Remove(CaseResults);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        //court fine list 
        [HttpGet("report/courtfines")]

        public Array courtfines(DateTime fromdate, DateTime todate, String station)
        {
            //get details from casefile docs
            IQueryable<CaseDocs> queryCaseDocs = _context.CaseDocs;
            var caseDocs = queryCaseDocs.Where(y => y.docdate >= fromdate && y.docdate <= todate).Select(x => new
            {
                x.id,
                x.casedocid,
                x.docname,
                x.docdate,
                x.casedetailsid
            }).ToList();
            //get all caseresults docs
            IQueryable<CaseDetails> queryCaseDetails = _context.CaseDetails;
            var caseDetails = queryCaseDetails.Where(y => y.casedatetime >= fromdate && y.casedatetime <= todate).Select(x => new
            {
                x.id,
                x.prohibitionorder,
                x.transportername,
                x.ticketaxletype,
                x.transporteradd,
                x.caseexcessload,
                x.caseticket,
                x.casedatetime,
            }).ToList();
            IQueryable<CaseResults> query = _context.CaseResults;
            var resultsData = query.Where(y => y.datetime >= fromdate && y.datetime <= todate).Select(x => new
            {
                x.id,
                x.receiptno,
                x.caseid,
                x.ownercharge,
                x.drivercharge,
                x.transportercharge,
                x.loadercharge,
                x.parkingfee,
                x.casereregno,
                x.casereticketno,
                x.datetime,
                x.createdby,
                x.casedetailsid,
            }).ToList().OrderBy(m => m.caseid);
            List<courtfineList> caseresults = new List<courtfineList>();
            var caseid = "";
           /* double transporterChargeVars = 0;
            double loaderChargeVars = 0;
            double driverChargeVars = 0;
            double parkingFeeVars = 0;
            string receipts = "";
            double ownerChargeVars = 0;
            int adder = 0;*/

            foreach (var results in resultsData)
            {//5
                try
                {
                    var detailedResults = caseDetails.Where(z => z.id == results.casedetailsid).FirstOrDefault();
                    var casedocsResults = caseDocs.Where(z => z.casedetailsid == results.casedetailsid).Where(m => m.docname == "Prohibition Order").FirstOrDefault();
                    var casedocsResultsCharge = caseDocs.Where(z => z.casedetailsid == results.casedetailsid).Where(m => m.docname == "Charge Sheet").FirstOrDefault();
                    var getOverloads = _context.WeighbridgeTransactions.Where(z => z.wbrg_ticket_no == detailedResults.caseticket).FirstOrDefault();
                    caseresults.Add(new courtfineList
                    {
                        transporterCharge = (double)results.transportercharge,
                        noPlate = results.casereregno,
                        receiptNo = results.receiptno,
                        loaderCharge = (double)results.loadercharge,
                        parkingCharge = (double)results.parkingfee,
                        wbtTicketNo = detailedResults.caseticket,
                        ownerCharge = (double)results.ownercharge,
                        prohibitionOrderNo = casedocsResults.casedocid,
                        totalCharge = (double)results.loadercharge + (double)results.parkingfee + (double)results.drivercharge + (double)results.ownercharge + (double)results.transportercharge,
                        axle = detailedResults.ticketaxletype,
                        excessAxle = getOverloads.wbrg_ticket_axleload,
                        driverCharge = (double)results.drivercharge,
                        excessGVW = getOverloads.wbrg_ticket_gvwload,
                        transporter = detailedResults.transportername,
                        transporterAddress = detailedResults.transporteradd,
                        paymentDate = results.datetime,
                        dateCharged = casedocsResultsCharge.docdate,
                    }); ;
               /*     caseid = results.caseid;
                    transporterChargeVars = 0;
                    driverChargeVars = 0;
                    parkingFeeVars = 0;
                    ownerChargeVars = 0;
                    receipts = "";
                    adder = 0;*/
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                /*  var myid= results.caseid;
                  if (caseid == "")
                  {
                      caseid = results.caseid;//54

                  }
                  if (caseid == results.caseid)
                  {
                      transporterChargeVars = transporterChargeVars + (double)results.transportercharge;
                      driverChargeVars = driverChargeVars + (double)(results.drivercharge);
                      parkingFeeVars = parkingFeeVars + (double)( results.parkingfee);
                      ownerChargeVars=ownerChargeVars + (double)(results.ownercharge);
                      loaderChargeVars = loaderChargeVars + (double)(results.loadercharge);
                      receipts = receipts + results.receiptno;
                      adder = 1;


                  }
                  else
                  {
                      if(adder==1)
                      {
                          try
                          {
                              var detailedResults = caseDetails.Where(z => z.id == results.casedetailsid).FirstOrDefault();
                              var casedocsResults = caseDocs.Where(z => z.casedetailsid == results.casedetailsid).Where(m => m.docname == "Prohibition Order").FirstOrDefault();
                              var casedocsResultsCharge = caseDocs.Where(z => z.casedetailsid == results.casedetailsid).Where(m => m.docname == "Charge Sheet").FirstOrDefault();
                              var getOverloads = _context.WeighbridgeTransactions.Where(z => z.wbrg_ticket_no == detailedResults.caseticket).FirstOrDefault();
                              caseresults.Add(new courtfineList
                              {
                                  transporterCharge = transporterChargeVars,
                                  noPlate = results.casereregno,
                                  receiptNo = receipts,
                                  loaderCharge = loaderChargeVars,
                                  parkingCharge =parkingFeeVars,
                                  wbtTicketNo = detailedResults.caseticket,
                                  ownerCharge = ownerChargeVars,
                                  prohibitionOrderNo = casedocsResults.casedocid,
                                  totalCharge =loaderChargeVars + parkingFeeVars +driverChargeVars + ownerChargeVars + transporterChargeVars,
                                  axle = detailedResults.ticketaxletype,
                                  excessAxle = getOverloads.wbrg_ticket_axleload,
                                  driverCharge = driverChargeVars,
                                  excessGVW = getOverloads.wbrg_ticket_gvwload,
                                  transporter = detailedResults.transportername,
                                  transporterAddress = detailedResults.transporteradd,
                                  paymentDate = results.datetime,
                                  dateCharged = casedocsResultsCharge.docdate,
                              });
                              caseid = results.caseid;
                              transporterChargeVars = 0;
                              driverChargeVars = 0;
                              parkingFeeVars = 0;
                              ownerChargeVars = 0;
                              receipts = "";
                              adder = 0;
                          }
                          catch (Exception ex)
                          {
                              ex.ToString();
                          }
                      }
                      else//if you didnt add anything
                      {
                          try
                          {
                              var detailedResults = caseDetails.Where(z => z.id == results.casedetailsid).FirstOrDefault();
                              var casedocsResults = caseDocs.Where(z => z.casedetailsid == results.casedetailsid).Where(m => m.docname == "Prohibition Order").FirstOrDefault();
                              var casedocsResultsCharge = caseDocs.Where(z => z.casedetailsid == results.casedetailsid).Where(m => m.docname == "Charge Sheet").FirstOrDefault();
                              var getOverloads = _context.WeighbridgeTransactions.Where(z => z.wbrg_ticket_no == detailedResults.caseticket).FirstOrDefault();
                              caseresults.Add(new courtfineList
                              {
                                  transporterCharge =(double) results.transportercharge,
                                  noPlate = results.casereregno,
                                  receiptNo = results.receiptno,
                                  loaderCharge = (double)results.loadercharge,
                                  parkingCharge = (double)results.parkingfee,
                                  wbtTicketNo = detailedResults.caseticket,
                                  ownerCharge = (double)results.ownercharge,
                                  prohibitionOrderNo = casedocsResults.casedocid,
                                  totalCharge = (double)results.loadercharge + (double)results.parkingfee + (double)results.drivercharge + (double)results.ownercharge + (double)results.transportercharge,
                                  axle = detailedResults.ticketaxletype,
                                  excessAxle = getOverloads.wbrg_ticket_axleload,
                                  driverCharge = (double)results.drivercharge,
                                  excessGVW = getOverloads.wbrg_ticket_gvwload,
                                  transporter = detailedResults.transportername,
                                  transporterAddress = detailedResults.transporteradd,
                                  paymentDate = results.datetime,
                                  dateCharged = casedocsResultsCharge.docdate,
                              }); ;
                              caseid = results.caseid;
                              transporterChargeVars = 0;
                              driverChargeVars = 0;
                              parkingFeeVars = 0;
                              ownerChargeVars = 0;
                              receipts = "";
                              adder = 0;
                          }
                          catch (Exception ex)
                          {
                              ex.ToString();
                          }
                      }

                  }


  */
            }



            return caseresults.ToArray();


        }
        [HttpGet("report/processlist")]

        public Array ProcessList(DateTime fromdate, DateTime todate, String station)
        {
            //get details from casefile docs
            IQueryable<CaseDocs> queryCaseDocs = _context.CaseDocs;
            var caseDocs = queryCaseDocs.Where(y => y.docdate >= fromdate && y.docdate <= todate).Select(x => new
            {
                x.id,
                x.casedocid,
                x.docname,
                x.docdate,
                x.casedetailsid
            }).ToList();
            //get all caseresults docs
            IQueryable<CaseDetails> queryCaseDetails = _context.CaseDetails;
            var caseDetails = queryCaseDetails.Where(y => y.casedatetime >= fromdate && y.casedatetime <= todate).Select(x => new
            {
                x.id,
                x.prohibitionorder,
                x.transportername,
                x.ticketaxletype,
                x.transporteradd,
                x.caseexcessload,
                x.caseticket,
                x.casedatetime,
            }).ToList();
            IQueryable<CaseResults> query = _context.CaseResults;
            var resultsData = query.Where(y => y.datetime >= fromdate && y.datetime <= todate).Select(x => new
            {
                x.id,
                x.receiptno,
                x.ownercharge,
                x.drivercharge,
                x.transportercharge,
                x.loadercharge,
                x.parkingfee,
                x.casereregno,
                x.casereticketno,
                x.datetime,
                x.createdby,
                x.casedetailsid,
            }).ToList().OrderBy(m => m.id);
            List<courtfineList> caseresults = new List<courtfineList>();
            int id = 0;
            double transporterChargeVars = 0;
            double driverChargeVars = 0;
            double parkingFeeVars = 0;
            string receipts = "";
            double ownerCharge = 0;

            foreach (var results in resultsData)
            {//5
                if (id == 0)
                {
                    id = results.id;
                }
                if (id == results.id)
                {
                    transporterChargeVars = transporterChargeVars + (double)results.transportercharge;
                    driverChargeVars = driverChargeVars + (double)(results.drivercharge);
                    parkingFeeVars = parkingFeeVars + (double)(results.parkingfee);
                    ownerCharge = ownerCharge + (double)(results.ownercharge);
                    receipts = receipts + results.receiptno;

                }
                else
                {
                    try
                    {
                        var detailedResults = caseDetails.Where(z => z.id == results.casedetailsid).FirstOrDefault();
                        var casedocsResults = caseDocs.Where(z => z.casedetailsid == results.casedetailsid).Where(m => m.docname == "Prohibition Order").FirstOrDefault();
                        var casedocsResultsCharge = caseDocs.Where(z => z.casedetailsid == results.casedetailsid).Where(m => m.docname == "Charge Sheet").FirstOrDefault();
                        var getOverloads = _context.WeighbridgeTransactions.Where(z => z.wbrg_ticket_no == detailedResults.caseticket).FirstOrDefault();
                        caseresults.Add(new courtfineList
                        {
                            transporterCharge = transporterChargeVars,
                            noPlate = results.casereregno,
                            receiptNo = results.receiptno,
                            loaderCharge = (double)results.loadercharge,
                            parkingCharge = (double)results.parkingfee,
                            wbtTicketNo = detailedResults.caseticket,
                            ownerCharge = (double)results.ownercharge,
                            prohibitionOrderNo = casedocsResults.casedocid,
                            totalCharge = (double)results.loadercharge + (double)results.parkingfee + (double)results.drivercharge + (double)results.ownercharge + (double)results.transportercharge,
                            axle = detailedResults.ticketaxletype,
                            excessAxle = getOverloads.wbrg_ticket_axleload,
                            driverCharge = (double)results.drivercharge,
                            excessGVW = getOverloads.wbrg_ticket_gvwload,
                            transporter = detailedResults.transportername,
                            transporterAddress = detailedResults.transporteradd,
                            paymentDate = results.datetime,
                            dateCharged = casedocsResultsCharge.docdate,
                        });
                        id = results.id;
                        transporterChargeVars = 0;
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }



            }



            return caseresults.ToArray();


        }
        private bool CaseResultsExists(int id)
        {
            return _context.CaseResults.Any(e => e.id == id);
        }
        
    }
}
