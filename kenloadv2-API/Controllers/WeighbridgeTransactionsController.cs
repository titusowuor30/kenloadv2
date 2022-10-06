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
using System.Linq.Expressions;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace KenloadV2API.Controllers
{
    class hourlyData {
        public String id { get; set; }
        public String station { get; set; }
        public String hourDate { get; set; }
        public String hourTime { get; set; }
        public String multideck { get; set; }
        public String singleAxle { get; set; }
        public String manually { get; set; }
        public String hswim { get; set; }
        public String calledIn { get; set; }
        public String calledInLess { get; set; }
        public String  warned { get; set; }
        public String charged { get; set; }
        public String cleared { get; set; }
        public String specialRelease { get; set; }
        public String redistributed { get; set; }
        public String totalWeighed { get; set; }

        public String impoundedandprohibited { get; set; }

    }
    class hourlyStatistics
    {
        public String id { get; set; }
        public String wbrg_ticket_no { get; set; }
        public String wbrg_station { get; set; }
        public String veh_reg { get; set; }
        public String wbrg_ticket_source { get; set; }
        public String wbrg_ticket_destination { get; set; }
        public String wbrg_ticket_date { get; set; }
        public String axle1_weight { get; set; }
        public String axle2_weight { get; set; }
        public String axle3_weight { get; set; }
        public String axle4_weight { get; set; }
        public String axle5_weight { get; set; }
        public String axle6_weight { get; set; }
        public String wbrg_ticket_grossweight { get; set; }
        public String wbrg_ticket_gvwload { get; set; }
        public String wbrg_ticket_dateout { get; set; }
        public String wbrg_ticket_charge_no { get; set; }
        public String wbrg_ticket_type { get; set; }
    }
    class weighedperaxle
    {
        public int itemNo { get; set; }
        public string axleConfig { get; set; }
        public int divertedPerAxle { get; set; }
        public int overloadedPerAxle { get; set; }
        public int chargedPerAxle { get; set; }

    }
    class weighedunderthrehshold
    {
        public int itemNo { get; set; }
        public string veh_reg { get; set; }
        public string axleConfiguration { get; set; } 
        public string transporter { get; set; }
        public string excessAxle { get; set; }
        public string gvwLoad { get; set; }
    }
    class habitualOffenders
    {
        public int itemNo { get; set; }
        public string transporterName { get; set; }
        public string transporteradd { get; set; }
        public int overLoaded { get; set; }
        public int charged { get; set; }
        public int cleared { get; set; }

    }

    class staticvshswim
    {
        public int id { get; set; }
        
        public int cid { get; set; }
        public DateTime date_time_local { get; set; }
        public int vehicle_class { get; set; }
        public string axle_conf { get; set; }
        public string lp { get; set; }
        public int axle_1_load { get; set; }
        public int axle_2_load { get; set; }
        public int axle_3_load { get; set; }
        public int axle_4_load { get; set; }
        public int axle_5_load { get; set; }
        public int axle_6_load { get; set; }
        public int axle_7_load { get; set; }
        public int axle_8_load { get; set; }
        public int axle_9_load { get; set; }
        public String calledin { get; set; }

        public DateTime wbrg_ticket_date { get; set; }
        public String wbrg_ticket_no { get; set; }
        public String wbrg_station { get; set; }
        public String veh_reg { get; set; }
        public String wbrg_ticket_source { get; set; }
        public String wbrg_ticket_destination { get; set; }
        public String wbrg_product { get; set; }
        public int axle1_weight { get; set; }
        public int axle2_weight { get; set; }
        public int axle3_weight { get; set; }
        public int axle4_weight { get; set; }
        public String status { get; set; }
        public String state { get; set; }

        public int wbrg_ticket_grossweight { get; set; }
        public int hswim_grossweight { get; set; }

        public int Diff { get; set; }

        public String calledinreason { get; set; }
        

    }
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeighbridgeTransactionsController : ControllerBase
    {
        private const string V = "000000";
        private readonly Kenloadv2Data _context;

        public WeighbridgeTransactionsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/WeighbridgeTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeighbridgeTransactions>>> GetWeighbridgeTransactions()
        {
            return await _context.WeighbridgeTransactions.OrderByDescending(a => a.id).ToListAsync();
        }

        // GET: api/WeighbridgeTransactions/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<WeighbridgeTransactions>> GetWeighbridgeTransactions(int id)
        {
            var weighbridgeTransactions = await _context.WeighbridgeTransactions.FindAsync(id);

            if (weighbridgeTransactions == null)
            {
                return NotFound();
            }

            return weighbridgeTransactions;
        }
 
        [HttpGet("max")]
        public String GetWeighbridgeTransactionsWBTno(String wbrg_ticket_no, String reweigh, String status, DateTime fromdate, DateTime todate)
        {
            var ticketno = V;

            if (wbrg_ticket_no != null)
            {//.Where(x => x.wbrg_ticket_no.Substring(0, wbrg_ticket_no.Length) == wbrg_ticket_no && x.wbrg_ticket_no.Length < 18)
                var tick = "0";
                try
                {
                    tick = _context.WeighbridgeTransactions.Where(x => x.wbrg_ticket_no.Substring(0, wbrg_ticket_no.Length) == wbrg_ticket_no && x.wbrg_ticket_no.Length < 18).OrderBy(y => y.id).LastOrDefault().wbrg_ticket_no.ToString().Substring(wbrg_ticket_no.Length) + "";
                }
                catch (Exception) 
                {
                    tick = "0";
                }
               
                //if (string.IsNullOrEmpty(tick))
                //{
                //    tick = "0";
                //}
                ticketno = Convert.ToInt32(tick) + "";
                return ticketno;
            }
            if (reweigh != null)
            {
                ticketno = _context.WeighbridgeTransactions.Where(x => x.wbrg_ticket_no.Substring(0, reweigh.Length) == reweigh).Count() + "";
                return ticketno;
            }
            if (status != null)
            {
                ticketno = _context.WeighbridgeTransactions.Where(x => x.wbrg_ticket_status == status).Count() + "";
                return ticketno;
            }

            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
            {
                var Selections = _context.WeighbridgeTransactions.Where(x => x.wbrg_ticket_date >= fromdate && x.wbrg_ticket_date <= todate).Count() + "";
                return Selections;
            }

            ticketno = _context.WeighbridgeTransactions.OrderBy(y => y.id).LastOrDefault().id + "";

            return ticketno;
        }
        //[HttpGet("hourcountdaterange")]
        //public String GetWeighbridgeTransactionsCount(DateTime fromdate, DateTime todate,int hour=-1)
        //{
        //    String[] hours = new String[24];
        //    if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM"))
        //    {
        //    IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
        //        IQueryable<WeighbridgeTransactions> querywim = (IQueryable<WeighbridgeTransactions>)_context.Wim;
        //        IQueryable<WeighbridgeTransactions> querymanual= (IQueryable<WeighbridgeTransactions>)_context.ManualWeighing;
        //        int hourcont = hours.Length;
        //    TimeSpan daycount = todate.Subtract(fromdate);
        //    int days = int.Parse(daycount.Days.ToString());
        //    String[,] results = new String[days, hourcont];


        //        for (int d = 0; d < days; d++)
        //        {
        //            //Add days to date
        //            DateTime datet =fromdate.AddDays(d);
        //            for (int h = 0; h < hours.Length; h++)
        //            {
        //                results[d, h] =  query.Where(x => x.wbrg_ticket_date.Hour >= h && x.wbrg_ticket_date == datet).Count()+"" ;
        //            }

        //        }
        //        return results.ToString();
        //    }


        //    return hours.ToString();
        //}
        public static Expression<Func<T, bool>> Make<T>() { return null; }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<WeighbridgeTransactions>>> GetWeighbridgeTransactionsSearch(String veh_reg, String transporter, String cargo, String station, String wbrg_ticket_no, String Status, String State, String caseid, String weighbrigeside, String asc, String allwbt, DateTime fromdate, DateTime todate, int offset, int exported = -1, int limit = 50)
        {
            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var predicate = PredicateBuilder.False<WeighbridgeTransactions>();

            //if (Status != null)
            //{
            //    query = query.Where(x => x.wbrg_ticket_status == Status);
            //}
            //if (State != null)
            //{
            //    query = query.Where(x => x.wbrg_ticket_state== State);
            //}
            //ParameterExpression pe = Expression.Parameter(typeof(WeighbridgeTransactions), "x");
            //    MemberExpression me = Expression.Property(pe, "wbrg_product");
            //    System.Linq.Expressions.MemberExpression memberExpression =
            //System.Linq.Expressions.Expression.Field(
            //    System.Linq.Expressions.Expression.Constant(me),
            //    "species");

            //    ConstantExpression constant = Expression.Constant(cargo);
            //    BinaryExpression body = Expression.Equal(me,constant) ;
            //    BinaryExpression body2 = Expression.Equal(me, constant);

            //var iscargo = 
            //Expression.Lambda<Func<WeighbridgeTransactions, bool>>(
            //    Expression.Or(
            //        Expression.Equal(Expression.Property(pe, "wbrg_product"), Expression.Constant(cargo)),
            //        Expression.Equal(Expression.Property(pe, "wbrg_product"), Expression.Constant(cargo))),
            //            new[] { pe });

            //query.Where(iscargo);
            if (cargo != null)
            {
                if (cargo.Contains(","))
                {
                    var sval = cargo.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.wbrg_product == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_product == cargo);
                }



            }
            if (transporter != null)
            {
                if (transporter.Contains(","))
                {
                    var sval = transporter.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.wbrg_tx_code == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_tx_code == transporter);
                }
            }

            if (exported >= 0)
            {

                query = query.Where(x => x.exported == exported);

            }

            if (Status != null)
            {
                if (Status.Contains(","))
                {
                    var sval = Status.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.wbrg_ticket_status == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_ticket_status == Status);
                }
            }


            if (State != null)
            {
                if (State.Contains(","))
                {
                    var sval = State.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.wbrg_ticket_state == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_ticket_state == State);
                }
            }

            if (weighbrigeside != null)
            {
                if (weighbrigeside.Contains(","))
                {
                    var sval = weighbrigeside.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.wbrg_ticket_weighbridge == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_ticket_weighbridge == weighbrigeside);
                }
            }

            //if (Status != null)
            //{
            //    query = query.Where(x => x.wbrg_ticket_case_id == null);
            //}


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
                        predicate = predicate.Or(x => x.wbrg_station == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_station == station);
                }

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
                        predicate = predicate.Or(x => x.veh_reg == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.veh_reg == veh_reg);
                }
            }
            if (wbrg_ticket_no != null)
            {
                if (allwbt != null)
                {
                    //query = query.Where(x => x.wbrg_ticket_no.Contains(wbrg_ticket_no));
                    query = query.Where(x => x.wbrg_ticket_no.Substring(0,wbrg_ticket_no.Length) == wbrg_ticket_no);

                }
                else
                {
                    if (wbrg_ticket_no.Contains(","))
                    {
                        var sval = wbrg_ticket_no.Split(",");

                        int clen = sval.Length;
                        int id = 0;
                        for (int g = 0; g < clen; g++)
                        {
                            String val = sval[id];
                            predicate = predicate.Or(x => x.wbrg_ticket_no == val);
                            id++;
                        }

                        query = query.Where(predicate);
                    }
                    else
                    {
                        query = query.Where(x => x.wbrg_ticket_no == wbrg_ticket_no);
                    }

                }
               

            }

            if ((fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM")) && veh_reg== null)
            {
                query = query.Where(x => x.wbrg_ticket_date >= fromdate && x.wbrg_ticket_date <= todate);
            }


            //if (wbrg_ticket_no != null || fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") || veh_reg != null || Status != null || transporter != null || cargo != null || caseid != null)
            //{
            //    return await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            //}
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            if (asc != null)
            {

            }
            else
            {
                query = query.OrderByDescending(a => a.wbrg_ticket_date);
            }

            var Selection = await query.Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }

        [HttpGet("{wbrg_ticket_no}")]
        public async Task<ActionResult<WeighbridgeTransactions>> GetWeighbridgeTransactions(String wbrg_ticket_no)
        {
            var ylist = await _context.WeighbridgeTransactions.Where(x => x.wbrg_ticket_no == wbrg_ticket_no).FirstOrDefaultAsync();

            if (ylist == null)
            {
                //return NotFound();
            }

            return ylist;
        }

        // PUT: api/WeighbridgeTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeighbridgeTransactions(int id, WeighbridgeTransactions weighbridgeTransactions)
        {
            if (id != weighbridgeTransactions.id)
            {
                return BadRequest();
            }

            _context.Entry(weighbridgeTransactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeighbridgeTransactionsExists(id))
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

        // POST: api/WeighbridgeTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeighbridgeTransactions>> PostWeighbridgeTransactions(WeighbridgeTransactions weighbridgeTransactions)
        {
            _context.WeighbridgeTransactions.Add(weighbridgeTransactions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeighbridgeTransactions", new { id = weighbridgeTransactions.id }, weighbridgeTransactions);
        }

        // DELETE: api/WeighbridgeTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeighbridgeTransactions(int id)
        {
            var weighbridgeTransactions = await _context.WeighbridgeTransactions.FindAsync(id);
            if (weighbridgeTransactions == null)
            {
                return NotFound();
            }

            //   _context.WeighbridgeTransactions.Remove(weighbridgeTransactions);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool WeighbridgeTransactionsExists(int id)
        {
            return _context.WeighbridgeTransactions.Any(e => e.id == id);
        }
        [HttpGet("report/vehicles/{fromdate}/{todate}")]
        public Array VehiclesReport(DateTime fromdate, DateTime todate)
        {

            List<String> CaptureRow = new List<String>();
            var resultsData = _context.WeighbridgeTransactions.Where(y => y.wbrg_ticket_date >= fromdate && y.wbrg_ticket_date <= todate).Select(x => new
            {
                x.id,
                x.wbrg_ticket_date,
                x.wbrg_ticket_type,
                x.veh_reg
            }).ToList().GroupBy(z => z.veh_reg);



            return resultsData.ToArray();
        }
        [HttpGet("report/hourcountdaterange")]

        public Array HourCountResults(DateTime fromdate, DateTime todate, String station)
        {
            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var predicate = PredicateBuilder.False<WeighbridgeTransactions>();


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
                        predicate = predicate.Or(x => x.wbrg_station == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_station == station);
                }
            }
            var resultsData = query.Where(y => y.wbrg_ticket_date >= fromdate && y.wbrg_ticket_date <= todate).Select(x => new
            {
                x.id,
                x.wbrg_ticket_date,
                x.wbrg_ticket_type,
                x.wbrg_ticket_reweigh_no,//reweigh equals to or greater than 1
                x.wbrg_ticket_status,
                x.wbrg_ticket_no,// O OVERLOAD, L legal W warned
                //x.wbrg_ticket_state //R redistributed S special release
                // charsheet on casedocs 
                //special release in special release table
            }).ToList();
            int rowid = 1;
            int countMultiDeck = 0;
            int totalMultideck = 0;
            int countLswim = 0;
            int totalLswim = 0;
            int countSingleaxle = 0;
            int totalSingleaxle = 0;
            int storeTimespan = 0;
            List<hourlyData> captureRow = new List<hourlyData>();
            DateTime dateTracker = fromdate;
            //track 24 hours before repeats start// change todate to be tracker+24hours if todate is still later
            while (DateTime.Compare(dateTracker, todate) < 0)//iterates till the last hour
            {
                foreach (var recordData in resultsData)
                {
                    TimeSpan hourDifference = recordData.wbrg_ticket_date - dateTracker;
                    storeTimespan = hourDifference.Days;
                    if (storeTimespan == 0 && hourDifference.Hours < 24)
                    {
                        if (recordData.wbrg_ticket_type == "M" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                        {
                            //count and add Mutildeck
                            countMultiDeck++;
                        }
                        if (recordData.wbrg_ticket_type == "L" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                        {
                            //count and add Mutildeck
                            countLswim++;
                        }
                        if (recordData.wbrg_ticket_type == "S" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                        {
                            //count and add Mutildeck
                            countSingleaxle++;
                        }
                    }
                }

                int TotalRecords = countLswim + countMultiDeck + countSingleaxle;
                String hourTimeProcess = "";
                if (dateTracker.Hour.ToString().Length == 1)
                {
                    hourTimeProcess = "0" + dateTracker.Hour.ToString();
                }
                else
                {
                    hourTimeProcess = dateTracker.Hour.ToString();
                }

                captureRow.Add(new hourlyData
                {
                    id = rowid.ToString(),
                    hourDate = dateTracker.ToShortDateString(),
                    hourTime = hourTimeProcess,
                    multideck = countMultiDeck.ToString(),
                    singleAxle = countSingleaxle.ToString(),
                    hswim = countLswim.ToString(),
                    totalWeighed = TotalRecords.ToString()
                });
                rowid++;
                totalMultideck = totalMultideck + countMultiDeck;
                countMultiDeck = 0;
                totalSingleaxle = totalSingleaxle + countSingleaxle;
                countSingleaxle = 0;
                totalLswim = totalLswim + countLswim;
                countLswim = 0;
                dateTracker = dateTracker.AddHours(1);
                //create row
                //change Row for data
                //append total to array onve done
                //end while loop 
            }
            int Totals = totalMultideck + totalSingleaxle + totalLswim;
            captureRow.Add(new hourlyData
            {
                id = "TOTAL",
                hourDate = "",
                hourTime = "",
                multideck = totalMultideck.ToString(),
                singleAxle = totalSingleaxle.ToString(),
                hswim = totalLswim.ToString(),
                totalWeighed = Totals.ToString()
            });

            return captureRow.ToArray();

        }
        //[HttpGet("image")]
        //public String LoadBase64(string base64)
        //{


        //    System.IO.Stream fs = FileUpload1.PostedFile.InputStream;
        //    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
        //    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
        //    return base64String;

        //}
        [HttpGet("report/hourstatistics")]

        public Array hourstatistics(DateTime fromdate, DateTime todate, String station)
        {
            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var predicate = PredicateBuilder.False<WeighbridgeTransactions>();

            IQueryable<VirtualTicket> queryv = _context.VirtualTicket;
            var predicatev = PredicateBuilder.False<VirtualTicket>();


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
                        predicate = predicate.Or(x => x.wbrg_station == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_station == station);
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
                        predicatev = predicatev.Or(x => x.wim == val);
                        id++;
                    }

                    queryv = queryv.Where(predicatev);
                }
                else
                {
                    queryv = queryv.Where(x => x.wim == station);
                }
            }
            var resultsData = query.Where(y => y.wbrg_ticket_date >= fromdate && y.wbrg_ticket_date <= todate).Select(x => new
            {
                x.id,
                x.wbrg_ticket_date,
                x.wbrg_ticket_type,
                x.wbrg_ticket_reweigh_no,//reweigh equals to or greater than 1
                x.wbrg_ticket_status,
                x.wbrg_ticket_state,
                x.wbrg_ticket_ref,
                x.wbrg_ticket_no,// O OVERLOAD, L legal W warned
                //x.wbrg_ticket_state //R redistributed S special release
                // charsheet on casedocs  docname count chargesheet within timelimit
                //special release in special release table
            }).ToList();
            var chargeData = _context.CaseDocs.Where(y => y.docdate >= fromdate && y.docdate <= todate).Select(x => new
            {
                x.id,
                x.docdate,
                x.caseid,
                x.casedocid,
                x.casedetailsid,
                x.docname,
            }).ToList();

            var calledInData = queryv.Where(y => y.date_time_local >= fromdate && y.date_time_local <= todate && (y.wim.Contains("W1")|| y.wim.Contains("W2") || y.wim.Contains("W3")) ).Select(x => new
            {
                x.id,
                x.calledin,
                x.date_time_local,
                x.vehicle_class,
            }).ToList();

            var manualData = _context.ManualWeighing.Where(y => y.date >= fromdate && y.date <= todate).Select(x => new
            {
                x.id,
                x.date,
                x.hour,
                x.tickets,
            }).ToList();

            int rowid = 1;
            int i = 0;
            int countMultiDeck = 0;
            int totalMultideck = 0;
            int countLswim = 0;
            int totalLswim = 0;
            int counthswimTotal = 0;
            int totalhswimTotal = 0;
            int countcalledIn = 0;
            int totalcalledIn = 0;
            int countSingleaxle = 0;
            int totalSingleaxle = 0;
            int countoverload = 0;
            int totaloverload = 0;
            int countwarned = 0;
            int countspecialrelease = 0;
            int totalspecialrelease = 0;
            int countredistribute = 0;
            int totalredistribute = 0;
            int totalwarned = 0;
            int countCharged = 0;
            int totalcharged = 0;
            int storeTimespan = 0;
            int countManual = 0;
            int totalManual = 0;
            int totalcalledInLess = 0;
            int countcalledInLess = 0;
            int totalimpoundedandprohibited = 0;
            List<hourlyData> captureRow = new List<hourlyData>();
            DateTime dateTracker = fromdate;
            //track 24 hours before repeats start// change todate to be tracker+24hours if todate is still later
            while (DateTime.Compare(dateTracker, todate) < 0)//iterates till the last hour
            {
                foreach (var recordData in resultsData)
                {
                    TimeSpan hourDifference = recordData.wbrg_ticket_date - dateTracker;
                    storeTimespan = hourDifference.Days;
                    if (storeTimespan == 0 && hourDifference.Hours < 24)
                    {
                        if (recordData.wbrg_ticket_type == "M" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours && string.IsNullOrEmpty(recordData.wbrg_ticket_ref)
                        {
                            //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                            countMultiDeck++;
                        }
                        if (recordData.wbrg_ticket_type == "L" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                        {
                            //count and add Mutildeck
                            countLswim++;
                        }
                        if (recordData.wbrg_ticket_type == "S" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                        {
                            //count and add Mutildeck
                            countSingleaxle++;
                        }
                        //if (recordData.wbrg_ticket_type == "L" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                        //{
                        //    //count and add Mutildeck
                        //    countSingleaxle++;
                        //}
                        /* if (recordData.wbrg_ticket_status == "O" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day)//hours within 24hours
                         {
                             //count and add Mutildeck
                             countoverload++;
                         }*/
                        if (recordData.wbrg_ticket_status == "W" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                        {
                            //count and add Mutildeck
                            countwarned++;
                        }
                        if (recordData.wbrg_ticket_state == "R" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                        {
                            //count and add Mutildeck
                            countredistribute++;
                        }
                        if (recordData.wbrg_ticket_state == "S" && recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day)//hours within 24hours
                        {
                            //count and add Mutildeck
                            countspecialrelease++;
                        }
                    }
                }

                foreach (var charge in chargeData)
                {
                    TimeSpan hourDifferenceCharged = charge.docdate - dateTracker;
                    storeTimespan = hourDifferenceCharged.Days;
                    if (storeTimespan == 0 && hourDifferenceCharged.Hours < 24)
                    {
                        if (charge.docname == "Charge Sheet" && charge.docdate.Hour == dateTracker.Hour && charge.docdate.Day == dateTracker.Day)//hours within 24hours
                        {
                            //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                            countCharged++;
                        }


                    }
                }
                foreach (var hswim in calledInData)
                {
                    TimeSpan hourDifferenceHswim = hswim.date_time_local - dateTracker;
                    storeTimespan = hourDifferenceHswim.Days;
                    if (storeTimespan == 0 && hourDifferenceHswim.Hours < 24)
                    {
                        if (hswim.date_time_local.Hour == dateTracker.Hour && hswim.date_time_local.Day == dateTracker.Day && (hswim.vehicle_class>=5 && hswim.vehicle_class!=30))//hours within 24hours
                        {
                            //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                            counthswimTotal++;
                        }
                        if (hswim.calledin == 1 && hswim.date_time_local.Hour == dateTracker.Hour && hswim.date_time_local.Day == dateTracker.Day)//hours within 24hours
                        {
                            //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                            countcalledIn++;
                        }


                    }
                }
                foreach (var manualtickets in manualData)
                {
                    TimeSpan hourDifferenceManual = manualtickets.date - dateTracker;
                    storeTimespan = hourDifferenceManual.Days;
                    if (storeTimespan == 0 && hourDifferenceManual.Hours < 24)
                    {
                        if (manualtickets.date.Hour == dateTracker.Hour && manualtickets.date.Day == dateTracker.Day)//hours within 24hours
                        {
                            //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                            countManual+= manualtickets.tickets;//
                        }


                    }
                }

                int TotalRecords = countLswim + countMultiDeck + countSingleaxle;
                String hourTimeProcess = "";
                if (dateTracker.Hour.ToString().Length == 1)
                {
                    hourTimeProcess = "0" + dateTracker.Hour.ToString();
                }
                else
                {
                    hourTimeProcess = dateTracker.Hour.ToString();
                }
                countcalledInLess = counthswimTotal - countcalledIn;
                var impoundedandprohibited = countredistribute + countCharged;
                var lsw = countLswim + countSingleaxle;
                captureRow.Add(new hourlyData
                {
                    id = rowid.ToString(),
                    hourDate = dateTracker.ToShortDateString(),
                    hourTime = hourTimeProcess,
                    multideck = countMultiDeck.ToString(),
                    singleAxle = lsw.ToString(),
                    hswim = counthswimTotal.ToString(),
                    totalWeighed = TotalRecords.ToString(),
                    manually = countManual.ToString(),
                    calledIn = countcalledIn.ToString(),
                    calledInLess = countcalledInLess.ToString(),
                    warned = countwarned.ToString(),
                    charged = countCharged.ToString(),
                    specialRelease = countspecialrelease.ToString(),
                    redistributed = countredistribute.ToString(),
                    impoundedandprohibited = impoundedandprohibited.ToString(),

                });
                rowid++;
                totalcalledIn = totalcalledIn + countcalledIn;

                totalcalledInLess = totalcalledInLess + countcalledInLess;
                countcalledInLess = 0;
                totalhswimTotal = totalhswimTotal + counthswimTotal;
                totalcharged = totalcharged + countCharged;
                countCharged = 0;
                totalMultideck = totalMultideck + countMultiDeck;
                countMultiDeck = 0;
                totalSingleaxle = totalSingleaxle + countLswim + countSingleaxle;
                countSingleaxle = 0;
                //totalLswim = totalLswim + countLswim;
                countLswim = 0;
                totalwarned = totalwarned + countwarned;
                countwarned = 0;
                totaloverload = totaloverload + countoverload;
                countoverload = 0;
                totalredistribute = totalredistribute + countredistribute;
                countredistribute = 0;
                totalspecialrelease = totalspecialrelease + countspecialrelease;
                countspecialrelease = 0;
                totalManual = totalManual + countManual;
                totalimpoundedandprohibited = totalredistribute + totalcharged;
                countManual = 0;
                dateTracker = dateTracker.AddHours(1);
                counthswimTotal = 0;
                countcalledIn = 0;
                //create row
                //change Row for data
                //append total to array onve done
                //end while loop 
            }
            int Totals = totalMultideck + totalSingleaxle + totalLswim;
            captureRow.Add(new hourlyData
            {
                id = "TOTAL",
                hourDate = "",
                hourTime = "",
                multideck = totalMultideck.ToString(),
                singleAxle = totalSingleaxle.ToString(),
                manually = totalManual.ToString(),
                totalWeighed = Totals.ToString(),
                calledIn = totalcalledIn.ToString(),
                calledInLess = totalcalledInLess.ToString(),
                warned = totalwarned.ToString(),
                charged = totalcharged.ToString(),
                hswim = totalhswimTotal.ToString(),
                specialRelease = totalspecialrelease.ToString(),
                redistributed = totalredistribute.ToString(),
                impoundedandprohibited = totalimpoundedandprohibited.ToString(),

            });

            return captureRow.ToArray();

        }
        [HttpGet("report/dailystatistics")]

        public Array dailystatistics(DateTime fromdate, DateTime todate, String station)
        {
            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var predicate = PredicateBuilder.False<WeighbridgeTransactions>();


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
                        predicate = predicate.Or(x => x.wbrg_station == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_station == station);
                }
            }
            var resultsData = query.Where(y => y.wbrg_ticket_date >= fromdate && y.wbrg_ticket_date <= todate).Select(x => new
            {
                x.id,
                x.wbrg_ticket_date,
                x.wbrg_ticket_type,
                x.wbrg_ticket_reweigh_no,//reweigh equals to or greater than 1
                x.wbrg_ticket_status,
                x.wbrg_ticket_state,
                x.wbrg_ticket_ref,
                x.wbrg_ticket_no,// O OVERLOAD, L legal W warned
                //x.wbrg_ticket_state //R redistributed S special release
                // charsheet on casedocs  docname count chargesheet within timelimit
                //special release in special release table
            }).ToList();
            var chargeData = _context.CaseDocs.Where(y => y.docdate >= fromdate && y.docdate <= todate).Select(x => new
            {
                x.id,
                x.docdate,
                x.caseid,
                x.casedocid,
                x.casedetailsid,
                x.docname,
            }).ToList();

            var calledInData = _context.VirtualTicket.Where(y => y.date_time_local >= fromdate && y.date_time_local <= todate && y.wim.Contains("W1")).Select(x => new
            {
                x.id,
                x.calledin,
                x.date_time_local,
            }).ToList();
            var caseDocuments = _context.CaseDocs.Join(_context.CaseDetails, cd => cd.caseid, cr => cr.caseid, (cd, cr) =>
               new
               {
                   transportername = cr.transportername,
                   casenoplate = cr.caseregno,
                   caseid = cr.caseid,
                   casedatetime = cr.casedatetime,
                   docname = cd.docname,
               }
           ).Where(y => y.casedatetime >= fromdate && y.casedatetime <= todate);
            var yardlist = _context.YardList.Join(_context.CaseDetails, yd => yd.vehreg, cr => cr.caseregno, (yd, cr) =>
               new
               {
                   vehreg = cr.caseregno,


               }
            );
            var cleared = caseDocuments.Join(yardlist, cds => cds.casenoplate, yz => yz.vehreg, (cds, yz) => new {
               transportername = cds.transportername,
               docname = cds.docname,
               vehreg = yz.vehreg
           }).Where(t => t.vehreg == null || t.vehreg == "").Where(p => p.docname == "Load Correction Memo").ToList();

            var manualData = _context.ManualWeighing.Where(y => y.date >= fromdate && y.date <= todate).Select(x => new
            {
                x.id,
                x.date,
                x.hour,
                x.tickets,
            }).ToList();

            int rowid = 1;
            int i = 0;
            int countMultiDeck = 0;
            int totalMultideck = 0;
            int countLswim = 0;
            int totalLswim = 0;
            int counthswimTotal = 0;
            int totalhswimTotal = 0;
            int countcalledIn = 0;
            int totalcalledIn = 0;
            int countSingleaxle = 0;
            int totalSingleaxle = 0;
            int countoverload = 0;
            int totaloverload = 0;
            int countwarned = 0;
            int countspecialrelease = 0;
            int totalspecialrelease = 0;
            int countredistribute = 0;
            int totalredistribute = 0;
            int totalwarned = 0;
            int countCharged = 0;
            int totalcharged = 0;
            int countManual = 0;
            int totalManual = 0;
            int totalcalledInLess = 0;
            int countcalledInLess = 0;
            int totalimpoundedandprohibited = 0;
            List<hourlyData> captureRow = new List<hourlyData>();
            DateTime dateTracker = fromdate;
            //track 24 hours before repeats start// change todate to be tracker+24hours if todate is still later
            while (DateTime.Compare(dateTracker, todate) < 0)//iterates till the last hour
            {
                foreach (var recordData in resultsData)
                {
                    //TimeSpan hourDifference = recordData.wbrg_ticket_date - dateTracker;
                    //storeTimespan = hourDifference.Days;
                    //if (storeTimespan == 0 && hourDifference.Hours < 24)
                    //{
                    if (recordData.wbrg_ticket_type == "M" && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                    {
                        //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                        countMultiDeck++;
                    }
                    if (recordData.wbrg_ticket_type == "L" && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                    {
                        //count and add Mutildeck
                        countLswim++;
                    }
                    if (recordData.wbrg_ticket_type == "S" && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                    {
                        //count and add Mutildeck
                        countSingleaxle++;
                    }
                    //if (recordData.wbrg_ticket_type == "L" && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                    //{
                    //    //count and add Mutildeck
                    //    countSingleaxle++;
                    //}
                    /* if (recordData.wbrg_ticket_status == "O" && recordData.wbrg_ticket_date.Day == dateTracker.Day)//hours within 24hours
                     {
                         //count and add Mutildeck
                         countoverload++;
                     }*/
                    if (recordData.wbrg_ticket_status == "W" && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                    {
                        //count and add Mutildeck
                        countwarned++;
                    }
                    if (recordData.wbrg_ticket_state == "R" && recordData.wbrg_ticket_date.Day == dateTracker.Day && !recordData.wbrg_ticket_no.Contains("_"))//hours within 24hours
                    {
                        //count and add Mutildeck
                        countredistribute++;
                    }
                    if (recordData.wbrg_ticket_state == "S" && recordData.wbrg_ticket_date.Day == dateTracker.Day)//hours within 24hours
                    {
                        //count and add Mutildeck
                        countspecialrelease++;
                    }
                    //}
                }

                foreach (var charge in chargeData)
                {
                    //TimeSpan hourDifferenceCharged = charge.docdate - dateTracker;
                    //storeTimespan = hourDifferenceCharged.Days;
                    //if (storeTimespan == 0 && hourDifferenceCharged.Hours < 24)
                    //{
                    if (charge.docname == "Charge Sheet" && charge.docdate.Day == dateTracker.Day)//hours within 24hours
                    {
                        //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                        countCharged++;
                    }


                    //}
                }
                foreach (var hswim in calledInData)
                {
                    //TimeSpan hourDifferenceHswim = hswim.date_time_local - dateTracker;
                    //storeTimespan = hourDifferenceHswim.Days;
                    //if (storeTimespan == 0 && hourDifferenceHswim.Hours < 24)
                    //{
                    if (hswim.date_time_local.Day == dateTracker.Day)//hours within 24hours
                    {
                        //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                        counthswimTotal++;
                    }
                    if (hswim.calledin == 1 && hswim.date_time_local.Day == dateTracker.Day)//hours within 24hours
                    {
                        //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                        countcalledIn++;
                    }


                    //}
                }
                foreach (var manualtickets in manualData)
                {
                    TimeSpan hourDifferenceManual = manualtickets.date - dateTracker;
                    //storeTimespan = hourDifferenceManual.Days;
                    //if (storeTimespan == 0 && hourDifferenceManual.Hours < 24)
                    //{
                    if (manualtickets.date.Day == dateTracker.Day)//hours within 24hours
                    {
                        //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                        countManual+=manualtickets.tickets;
                    }


                    //}
                }

                int TotalRecords = countLswim + countMultiDeck + countSingleaxle;
                //String hourTimeProcess = "";
                //if (dateTracker.Hour.ToString().Length == 1)
                //{
                //    hourTimeProcess = "0" + dateTracker.Hour.ToString();
                //}
                //else
                //{
                //    hourTimeProcess = dateTracker.Hour.ToString();
                //}
                countcalledInLess = counthswimTotal - countcalledIn;
                var impoundedandprohibited = countredistribute + countCharged;
                captureRow.Add(new hourlyData
                {
                    id = rowid.ToString(),
                    hourDate = dateTracker.ToShortDateString(),
                    multideck = countMultiDeck.ToString(),
                    singleAxle = countSingleaxle.ToString(),
                    hswim = counthswimTotal.ToString(),
                    totalWeighed = TotalRecords.ToString(),
                    manually = countManual.ToString(),
                    calledIn = countcalledIn.ToString(),
                    calledInLess = countcalledInLess.ToString(),
                    warned = countwarned.ToString(),
                    charged = countCharged.ToString(),
                    specialRelease = countspecialrelease.ToString(),
                    redistributed = countredistribute.ToString(),
                    impoundedandprohibited = impoundedandprohibited.ToString(),

                });
                rowid++;
                totalcalledIn = totalcalledIn + countcalledIn;

                totalcalledInLess = totalcalledInLess + countcalledInLess;
                countcalledInLess = 0;
                totalhswimTotal = totalhswimTotal + counthswimTotal;
                totalcharged = totalcharged + countCharged;
                countCharged = 0;
                totalMultideck = totalMultideck + countMultiDeck;
                countMultiDeck = 0;
                totalSingleaxle = totalSingleaxle + countSingleaxle;
                countSingleaxle = 0;
                totalLswim = totalLswim + countLswim;
                countLswim = 0;
                totalwarned = totalwarned + countwarned;
                countwarned = 0;
                totaloverload = totaloverload + countoverload;
                countoverload = 0;
                totalredistribute = totalredistribute + countredistribute;
                countredistribute = 0;
                totalspecialrelease = totalspecialrelease + countspecialrelease;
                countspecialrelease = 0;
                totalManual = totalManual + countManual;
                totalimpoundedandprohibited += countredistribute + countCharged;
                countManual = 0;
                dateTracker = dateTracker.AddHours(24);
                counthswimTotal = 0;
                countcalledIn = 0;
                //create row
                //change Row for data
                //append total to array onve done
                //end while loop 
            }
            int Totals = totalMultideck + totalSingleaxle + totalLswim;
            captureRow.Add(new hourlyData
            {
                id = "TOTAL",
                hourDate = "",
                multideck = totalMultideck.ToString(),
                singleAxle = totalSingleaxle.ToString(),
                manually = totalManual.ToString(),
                totalWeighed = Totals.ToString(),
                calledIn = totalcalledIn.ToString(),
                calledInLess = totalcalledInLess.ToString(),
                warned = totalwarned.ToString(),
                charged = totalcharged.ToString(),
                hswim = totalhswimTotal.ToString(),
                specialRelease = totalspecialrelease.ToString(),
                redistributed = totalredistribute.ToString(),
                impoundedandprohibited = totalimpoundedandprohibited.ToString(),

            });

            return captureRow.ToArray();

        }
        [HttpGet("report/staticvshswim")]

        public Array staticvshswim(DateTime fromdate, DateTime todate, String station, String wim, int calledin = -1)
        {



            IQueryable<VirtualTicket> queryhswim = _context.VirtualTicket.Where(x => x.date_time_local >= fromdate && x.date_time_local <= todate);
            var predicate = PredicateBuilder.False<VirtualTicket>();

            List<staticvshswim> captureRow = new List<staticvshswim>();

            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var predicateW = PredicateBuilder.False<WeighbridgeTransactions>();


            if (station != null)
            {
                if (station.Contains(","))
                {
                    var sval = station.Split(",");

                    int clen = sval.Length;
                    int ids = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[ids];
                        predicate = predicate.Or(x => x.wim == val);
                        ids++;
                    }

                    queryhswim = queryhswim.Where(predicate);
                }
                else
                {
                    queryhswim = queryhswim.Where(x => x.wim == station);
                }
            }

            


            if (wim != null)
            {

                if (wim.Contains(","))
                {
                    var sval = wim.Split(",");

                    int clen = sval.Length;
                   int id3 = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id3];
                        predicate = predicate.Or(x => x.wim.Contains(val));
                        id3++;
                    }

                    queryhswim = queryhswim.Where(predicate);
                }
                else
                {
                    queryhswim = queryhswim.Where(x => x.wim.Contains(wim));
                }
            }

            if (calledin >= 0)
            {
                queryhswim = queryhswim.Where(x => x.calledin == calledin );
            }

            

            
            String[] rasdata = new String[queryhswim.Count()];
            //String[][] ras = new String[queryhswim.Count()][];
            String[,] ras = new String[queryhswim.Count(), 13];
            int i = 0;
            if (calledin >= 0)
            {
                foreach (var hswim in queryhswim)
                {

                    rasdata[i] = hswim.wbrg_ticket_no;
                    i++;
                }

                for (int g = 0; g < rasdata.Length; g++)
                {
                    if (rasdata[g] != null)
                    {
                        query = query.Where(x => x.wbrg_ticket_no == rasdata[g]);
                        foreach (var weighstatic in query)
                        {
                            
                            ras[g,0] = rasdata[g];
                            ras[g,1] = weighstatic.axle1_weight + "";
                            ras[g,2] = weighstatic.axle2_weight + "";
                            ras[g,3] = weighstatic.axle3_weight + "";
                            ras[g,4] = weighstatic.axle4_weight + "";
                            ras[g,5] = weighstatic.wbrg_product;
                            ras[g,6] = weighstatic.wbrg_ticket_status;
                            ras[g,7] = weighstatic.wbrg_ticket_state;
                            ras[g, 8] = weighstatic.wbrg_ticket_date+"";
                            ras[g, 9] = weighstatic.wbrg_station;
                            ras[g, 10] = weighstatic.veh_reg;
                            ras[g, 11] = weighstatic.wbrg_ticket_destination;
                            ras[g, 12] = weighstatic.wbrg_ticket_source;
                        }

                    }
                }
              
            }

            int id = 0;

            foreach (var hswim in queryhswim)
            {
                id++;
                int axle1_weight = 0;
                int axle2_weight = 0;
                int axle3_weight = 0;
                int axle4_weight = 0;
                DateTime wbrg_ticket_date = new DateTime();
                String wbrg_ticket_no = "";
                String wbrg_station = "";
                String veh_reg = "";
                String wbrg_ticket_source = "";
                String wbrg_ticket_destination = "";
                String wbrg_product = "";
                String wbrg_ticket_status = "";
                String wbrg_ticket_state = "";

                int axle_1_load = 0;
                int axle_2_load = 0;
                int axle_3_load = 0;
                int axle_4_load = 0;
                int axle_5_load = 0;
                int axle_6_load = 0;
                int axle_7_load = 0;
                int axle_8_load = 0;
                int axle_9_load = 0;

                
                if (hswim.wbrg_ticket_no != null)
                {
                        int a = 0;
                        try
                        {
                            for (int g = 0; g < ras.Length; g++)
                            {
                                
                            if (hswim.wbrg_ticket_no == ras[g,0])
                            {
                                wbrg_ticket_no = ras[g, 0];
                                axle1_weight = int.Parse(ras[g,1]);
                                axle2_weight = int.Parse(ras[g,2]);
                                axle3_weight = int.Parse(ras[g,3]);
                                axle4_weight = int.Parse(ras[g,4]);
                                wbrg_product = ras[g,5];
                                wbrg_ticket_status = ras[g,6];
                                wbrg_ticket_state = ras[g,7];

                                wbrg_ticket_date= DateTime.Parse(ras[g, 8]);
                                 wbrg_station= ras[g, 9];
                                  veh_reg= ras[g, 10];
                                 wbrg_ticket_destination= ras[g, 11];
                                 wbrg_ticket_source=ras[g, 12];
                            }    

                        }
                       


                    }
                    catch (Exception e)
                    {

                    }

                }





                axle_1_load = (int)hswim.axle_1_load;
                axle_2_load = (int)hswim.axle_2_load;
                axle_3_load = (int)hswim.axle_3_load;
                axle_4_load = (int)hswim.axle_4_load;
                axle_5_load = (int)hswim.axle_5_load;
                axle_6_load = (int)hswim.axle_6_load;
                axle_7_load = (int)hswim.axle_7_load;
                axle_8_load = (int)hswim.axle_8_load;
                axle_9_load = (int)hswim.axle_9_load;


                int diff = 0;

                int wbrg_ticket_grossweight = axle1_weight + axle2_weight + axle3_weight + axle4_weight;
                int hswim_grossweight = axle_1_load + axle_2_load + axle_3_load + axle_4_load + axle_5_load + axle_6_load + axle_7_load + axle_8_load + axle_9_load;
                if (wbrg_ticket_grossweight> 0 && hswim_grossweight > 0)
                {
                   diff = (int)(wbrg_ticket_grossweight - hswim_grossweight);
                }
                    
                String axle_conf = "";
                try
                {
                    if (hswim.axle_conf != null)
                    {

                        axle_conf = hswim.axle_conf.Trim();
                    }
                }
                catch (Exception e)
                {

                }

                String calledi = "NO";
                
                if (hswim.calledin == 1)
                {
                    calledi = "YES";
                }
                captureRow.Add(new staticvshswim
                {

                    id = id,
                    cid = hswim.cid,
                    date_time_local = hswim.date_time_local,
                    vehicle_class = hswim.vehicle_class,
                    axle_conf = axle_conf,
                    lp = hswim.lp,
                    axle_1_load = axle_1_load,
                    axle_2_load = axle_2_load,
                    axle_3_load = axle_3_load,
                    axle_4_load = axle_4_load,
                    axle_5_load = axle_5_load,
                    axle_6_load = axle_6_load,
                    axle_7_load = axle_7_load,
                    axle_8_load = axle_8_load,
                    axle_9_load = axle_9_load,
                    calledin = calledi,

                    wbrg_ticket_date = wbrg_ticket_date,
                    wbrg_ticket_no = wbrg_ticket_no,
                    wbrg_station = wbrg_station,
                    veh_reg = veh_reg,
                    wbrg_ticket_source = wbrg_ticket_source,
                    wbrg_ticket_destination = wbrg_ticket_destination,
                    axle1_weight = axle1_weight,
                    axle2_weight = axle2_weight,
                    axle3_weight = axle3_weight,
                    axle4_weight = axle4_weight,
                    wbrg_product = wbrg_product,
                    status = wbrg_ticket_status,
                    state = wbrg_ticket_state,
                    wbrg_ticket_grossweight = wbrg_ticket_grossweight,
                    hswim_grossweight = hswim_grossweight,
                    Diff = diff,
                    calledinreason = hswim.calledinreason,
                });

            }

            return captureRow.ToArray();

        }

        [HttpGet("report/hourlystatistics")]

        public Array HourlyRecords(DateTime fromdate, DateTime todate, String station)
        {
            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var predicate = PredicateBuilder.False<WeighbridgeTransactions>();


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
                        predicate = predicate.Or(x => x.wbrg_station == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_station == station);
                }
            }
            var resultsData = query.Where(y => y.wbrg_ticket_date >= fromdate && y.wbrg_ticket_date <= todate).Select(x => new
            {
                x.id,
                x.wbrg_ticket_no,
                x.wbrg_station,
                x.veh_reg,
                x.wbrg_ticket_source,
                x.wbrg_ticket_destination,
                x.wbrg_ticket_date,
                x.axle1_weight,
                x.axle2_weight,
                x.axle3_weight,
                x.axle4_weight,
                x.axle5_weight,
                x.axle6_weight,
                x.wbrg_ticket_grossweight,
                x.wbrg_ticket_gvwload,
                x.wbrg_ticket_dateout,
                x.wbrg_ticket_charge_no,
                x.wbrg_ticket_type,

            }).ToList();
            int rowid = 1;
            int storeTimespan = 0;
            List<hourlyStatistics> captureRow = new List<hourlyStatistics>();
            DateTime dateTracker = fromdate;
            //track 24 hours before repeats start// change todate to be tracker+24hours if todate is still later
            while (DateTime.Compare(dateTracker, todate) < 0)//iterates till the last hour
            {
                String hourTimeProcess = "";
                if (dateTracker.Hour.ToString().Length == 1)
                {
                    hourTimeProcess = "0" + dateTracker.Hour.ToString();
                }
                else
                {
                    hourTimeProcess = dateTracker.Hour.ToString();
                }
                foreach (var recordData in resultsData)
                {
                    TimeSpan hourDifference = recordData.wbrg_ticket_date - dateTracker;
                    storeTimespan = hourDifference.Days;


                    if (storeTimespan == 0 && hourDifference.Hours < 24)
                    {

                        if (recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !string.IsNullOrEmpty(recordData.veh_reg))//hours within 24hours
                        {
                            if (rowid == 1)
                            {
                                captureRow.Add(new hourlyStatistics
                                {
                                    id = "HOUR: " + hourTimeProcess,
                                    wbrg_ticket_no = "",
                                    wbrg_station = "",
                                    veh_reg = "",
                                    wbrg_ticket_source = "",
                                    wbrg_ticket_destination = "",
                                    wbrg_ticket_date = "",
                                    axle1_weight = "",
                                    axle2_weight = "",
                                    axle3_weight = "",
                                    axle4_weight = "",
                                    axle5_weight = "",
                                    axle6_weight = "",
                                    wbrg_ticket_grossweight = "",
                                    wbrg_ticket_gvwload = "",
                                    wbrg_ticket_dateout = "",
                                    wbrg_ticket_charge_no = "",
                                    wbrg_ticket_type = ""
                                });
                            }
                            captureRow.Add(new hourlyStatistics
                            {
                                id = rowid.ToString(),
                                wbrg_ticket_no = recordData.wbrg_ticket_no,
                                wbrg_station = recordData.wbrg_station,
                                veh_reg = recordData.veh_reg,
                                wbrg_ticket_source = recordData.wbrg_ticket_source,
                                wbrg_ticket_destination = recordData.wbrg_ticket_destination,
                                wbrg_ticket_date = recordData.wbrg_ticket_date.ToString(),
                                axle1_weight = recordData.axle1_weight.ToString(),
                                axle2_weight = recordData.axle2_weight.ToString(),
                                axle3_weight = recordData.axle3_weight.ToString(),
                                axle4_weight = recordData.axle4_weight.ToString(),
                                axle5_weight = recordData.axle5_weight.ToString(),
                                axle6_weight = recordData.axle6_weight.ToString(),
                                wbrg_ticket_grossweight = recordData.wbrg_ticket_grossweight.ToString(),
                                wbrg_ticket_gvwload = recordData.wbrg_ticket_gvwload.ToString(),
                                wbrg_ticket_dateout = recordData.wbrg_ticket_dateout.ToString(),
                                wbrg_ticket_charge_no = recordData.wbrg_ticket_charge_no,
                                wbrg_ticket_type = recordData.wbrg_ticket_type

                            });
                            rowid++;
                        }

                    }

                }
                dateTracker = dateTracker.AddHours(1);

                if (rowid > 1)
                {
                    captureRow.Add(new hourlyStatistics
                    {
                        id = "",
                        wbrg_ticket_no = "",
                        wbrg_station = "",
                        veh_reg = "",
                        wbrg_ticket_source = "",
                        wbrg_ticket_destination = "",
                        wbrg_ticket_date = "",
                        axle1_weight = "",
                        axle2_weight = "",
                        axle3_weight = "",
                        axle4_weight = "",
                        axle5_weight = "",
                        axle6_weight = "",
                        wbrg_ticket_grossweight = "",
                        wbrg_ticket_gvwload = "",
                        wbrg_ticket_dateout = "",
                        wbrg_ticket_charge_no = "TOTAL TICKETS for HOUR: " + hourTimeProcess + "=" + rowid,
                        wbrg_ticket_type = ""
                    });
                }

                rowid = 1;
                //create row
                //change Row for data
                //append total to array onve done
                //end while loop 
            }

            return captureRow.ToArray();

        }

        [HttpGet("report/weighedmanualcount")]

        public Array manualcount(DateTime fromdate, DateTime todate, String station)
        {
            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var predicate = PredicateBuilder.False<WeighbridgeTransactions>();


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
                        predicate = predicate.Or(x => x.wbrg_station == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_station == station);
                }
            }
            var resultsData = query.Where(y => y.wbrg_ticket_date >= fromdate && y.wbrg_ticket_date <= todate).Select(x => new
            {
                x.id,
                x.wbrg_ticket_no,
                x.wbrg_station,
                x.veh_reg,
                x.wbrg_ticket_source,
                x.wbrg_ticket_destination,
                x.wbrg_ticket_date,
                x.axle1_weight,
                x.axle2_weight,
                x.axle3_weight,
                x.axle4_weight,
                x.axle5_weight,
                x.axle6_weight,
                x.wbrg_ticket_grossweight,
                x.wbrg_ticket_gvwload,
                x.wbrg_ticket_dateout,
                x.wbrg_ticket_charge_no,
                x.wbrg_ticket_type,

            }).ToList();
            int rowid = 1;
            int storeTimespan = 0;
            List<hourlyStatistics> captureRow = new List<hourlyStatistics>();
            DateTime dateTracker = fromdate;
            //track 24 hours before repeats start// change todate to be tracker+24hours if todate is still later
            while (DateTime.Compare(dateTracker, todate) < 0)//iterates till the last hour
            {
                String hourTimeProcess = "";
                if (dateTracker.Hour.ToString().Length == 1)
                {
                    hourTimeProcess = "0" + dateTracker.Hour.ToString();
                }
                else
                {
                    hourTimeProcess = dateTracker.Hour.ToString();
                }
                foreach (var recordData in resultsData)
                {
                    TimeSpan hourDifference = recordData.wbrg_ticket_date - dateTracker;
                    storeTimespan = hourDifference.Days;


                    if (storeTimespan == 0 && hourDifference.Hours < 24)
                    {

                        if (recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !string.IsNullOrEmpty(recordData.veh_reg))//hours within 24hours
                        {
                            if (rowid == 1)
                            {
                                captureRow.Add(new hourlyStatistics
                                {
                                    id = "HOUR: " + hourTimeProcess,
                                    wbrg_ticket_no = "",
                                    wbrg_station = "",
                                    veh_reg = "",
                                    wbrg_ticket_source = "",
                                    wbrg_ticket_destination = "",
                                    wbrg_ticket_date = "",
                                    axle1_weight = "",
                                    axle2_weight = "",
                                    axle3_weight = "",
                                    axle4_weight = "",
                                    axle5_weight = "",
                                    axle6_weight = "",
                                    wbrg_ticket_grossweight = "",
                                    wbrg_ticket_gvwload = "",
                                    wbrg_ticket_dateout = "",
                                    wbrg_ticket_charge_no = "",
                                    wbrg_ticket_type = ""
                                });
                            }
                            captureRow.Add(new hourlyStatistics
                            {
                                id = rowid.ToString(),
                                wbrg_ticket_no = recordData.wbrg_ticket_no,
                                wbrg_station = recordData.wbrg_station,
                                veh_reg = recordData.veh_reg,
                                wbrg_ticket_source = recordData.wbrg_ticket_source,
                                wbrg_ticket_destination = recordData.wbrg_ticket_destination,
                                wbrg_ticket_date = recordData.wbrg_ticket_date.ToString(),
                                axle1_weight = recordData.axle1_weight.ToString(),
                                axle2_weight = recordData.axle2_weight.ToString(),
                                axle3_weight = recordData.axle3_weight.ToString(),
                                axle4_weight = recordData.axle4_weight.ToString(),
                                axle5_weight = recordData.axle5_weight.ToString(),
                                axle6_weight = recordData.axle6_weight.ToString(),
                                wbrg_ticket_grossweight = recordData.wbrg_ticket_grossweight.ToString(),
                                wbrg_ticket_gvwload = recordData.wbrg_ticket_gvwload.ToString(),
                                wbrg_ticket_dateout = recordData.wbrg_ticket_dateout.ToString(),
                                wbrg_ticket_charge_no = recordData.wbrg_ticket_charge_no,
                                wbrg_ticket_type = recordData.wbrg_ticket_type

                            });
                            rowid++;
                        }

                    }

                }
                dateTracker = dateTracker.AddHours(1);

                if (rowid > 1)
                {
                    captureRow.Add(new hourlyStatistics
                    {
                        id = "",
                        wbrg_ticket_no = "",
                        wbrg_station = "",
                        veh_reg = "",
                        wbrg_ticket_source = "",
                        wbrg_ticket_destination = "",
                        wbrg_ticket_date = "",
                        axle1_weight = "",
                        axle2_weight = "",
                        axle3_weight = "",
                        axle4_weight = "",
                        axle5_weight = "",
                        axle6_weight = "",
                        wbrg_ticket_grossweight = "",
                        wbrg_ticket_gvwload = "",
                        wbrg_ticket_dateout = "",
                        wbrg_ticket_charge_no = "TOTAL TICKETS for HOUR: " + hourTimeProcess + "=" + rowid,
                        wbrg_ticket_type = ""
                    });
                }

                rowid = 1;
                //create row
                //change Row for data
                //append total to array onve done
                //end while loop 
            }



            return captureRow.ToArray();

        }
        [HttpGet("report/overloaded")]

        public Array overloaded(DateTime fromdate, DateTime todate, String station)
        {
            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var predicate = PredicateBuilder.False<WeighbridgeTransactions>();


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
                        predicate = predicate.Or(x => x.wbrg_station == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_station == station);
                }
            }

            var resultsData = query.Where(y => y.wbrg_ticket_date >= fromdate && y.wbrg_ticket_date <= todate).Select(x => new
            {
                x.id,
                x.wbrg_ticket_no,
                x.wbrg_station,
                x.veh_reg,
                x.wbrg_ticket_source,
                x.wbrg_ticket_destination,
                x.wbrg_ticket_date,
                x.axle1_weight,
                x.axle2_weight,
                x.axle3_weight,
                x.axle4_weight,
                x.axle5_weight,
                x.axle6_weight,
                x.wbrg_ticket_grossweight,
                x.wbrg_ticket_gvwload,
                x.wbrg_ticket_dateout,
                x.wbrg_ticket_charge_no,
                x.wbrg_ticket_type,

            }).ToList();
            int rowid = 1;
            int storeTimespan = 0;
            List<hourlyStatistics> captureRow = new List<hourlyStatistics>();
            DateTime dateTracker = fromdate;
            //track 24 hours before repeats start// change todate to be tracker+24hours if todate is still later
            while (DateTime.Compare(dateTracker, todate) < 0)//iterates till the last hour
            {
                String hourTimeProcess = "";
                if (dateTracker.Hour.ToString().Length == 1)
                {
                    hourTimeProcess = "0" + dateTracker.Hour.ToString();
                }
                else
                {
                    hourTimeProcess = dateTracker.Hour.ToString();
                }
                foreach (var recordData in resultsData)
                {
                    TimeSpan hourDifference = recordData.wbrg_ticket_date - dateTracker;
                    storeTimespan = hourDifference.Days;


                    if (storeTimespan == 0 && hourDifference.Hours < 24)
                    {

                        if (recordData.wbrg_ticket_date.Hour == dateTracker.Hour && recordData.wbrg_ticket_date.Day == dateTracker.Day && !string.IsNullOrEmpty(recordData.veh_reg))//hours within 24hours
                        {
                            if (rowid == 1)
                            {
                                captureRow.Add(new hourlyStatistics
                                {
                                    id = "HOUR: " + hourTimeProcess,
                                    wbrg_ticket_no = "",
                                    wbrg_station = "",
                                    veh_reg = "",
                                    wbrg_ticket_source = "",
                                    wbrg_ticket_destination = "",
                                    wbrg_ticket_date = "",
                                    axle1_weight = "",
                                    axle2_weight = "",
                                    axle3_weight = "",
                                    axle4_weight = "",
                                    axle5_weight = "",
                                    axle6_weight = "",
                                    wbrg_ticket_grossweight = "",
                                    wbrg_ticket_gvwload = "",
                                    wbrg_ticket_dateout = "",
                                    wbrg_ticket_charge_no = "",
                                    wbrg_ticket_type = ""
                                });
                            }
                            captureRow.Add(new hourlyStatistics
                            {
                                id = rowid.ToString(),
                                wbrg_ticket_no = recordData.wbrg_ticket_no,
                                wbrg_station = recordData.wbrg_station,
                                veh_reg = recordData.veh_reg,
                                wbrg_ticket_source = recordData.wbrg_ticket_source,
                                wbrg_ticket_destination = recordData.wbrg_ticket_destination,
                                wbrg_ticket_date = recordData.wbrg_ticket_date.ToString(),
                                axle1_weight = recordData.axle1_weight.ToString(),
                                axle2_weight = recordData.axle2_weight.ToString(),
                                axle3_weight = recordData.axle3_weight.ToString(),
                                axle4_weight = recordData.axle4_weight.ToString(),
                                axle5_weight = recordData.axle5_weight.ToString(),
                                axle6_weight = recordData.axle6_weight.ToString(),
                                wbrg_ticket_grossweight = recordData.wbrg_ticket_grossweight.ToString(),
                                wbrg_ticket_gvwload = recordData.wbrg_ticket_gvwload.ToString(),
                                wbrg_ticket_dateout = recordData.wbrg_ticket_dateout.ToString(),
                                wbrg_ticket_charge_no = recordData.wbrg_ticket_charge_no,
                                wbrg_ticket_type = recordData.wbrg_ticket_type

                            });
                            rowid++;
                        }

                    }

                }
                dateTracker = dateTracker.AddHours(1);

                if (rowid > 1)
                {
                    captureRow.Add(new hourlyStatistics
                    {
                        id = "",
                        wbrg_ticket_no = "",
                        wbrg_station = "",
                        veh_reg = "",
                        wbrg_ticket_source = "",
                        wbrg_ticket_destination = "",
                        wbrg_ticket_date = "",
                        axle1_weight = "",
                        axle2_weight = "",
                        axle3_weight = "",
                        axle4_weight = "",
                        axle5_weight = "",
                        axle6_weight = "",
                        wbrg_ticket_grossweight = "",
                        wbrg_ticket_gvwload = "",
                        wbrg_ticket_dateout = "",
                        wbrg_ticket_charge_no = "TOTAL TICKETS for HOUR: " + hourTimeProcess + "=" + rowid,
                        wbrg_ticket_type = ""
                    });
                }

                rowid = 1;
                //create row
                //change Row for data
                //append total to array onve done
                //end while loop 
            }
            return captureRow.ToArray();

        }
        [HttpGet("report/vehicleweighedperaxle")]

        public Array Weighedperaxle(DateTime fromdate, DateTime todate, String station)
        {
            List<weighedperaxle> weighedperaxleList = new List<weighedperaxle>();
            //find all axle configs
            var allAxleConfigs = _context.AxleWeightConfig.ToList();
            var sanitizedConfigs = new HashSet<string>();
            foreach (var config in allAxleConfigs)
            {

                sanitizedConfigs.Add(config.axle_code.Substring(0, 2));
                //store all the configs

            }
            //count the configurations
            //store sets in a hashtable
            Dictionary<string, int> countAxle = new Dictionary<string, int>();
            foreach (var config in sanitizedConfigs)
            {
                countAxle.Add(config, 0);

            }
            //
            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var resultsData = query.Where(y => y.wbrg_ticket_date >= fromdate && y.wbrg_ticket_date <= todate).Where(z => !string.IsNullOrEmpty(z.wbrg_ticket_axel)).Select(x => new
            {
                x.id,
                x.wbrg_ticket_axel,

            }).ToList();
            foreach (var transaction in resultsData)
            {
                string axleType = transaction.wbrg_ticket_axel.Substring(0, 2);

                if (countAxle.ContainsKey(axleType))
                {
                    countAxle[axleType] += 1;
                }

            }
            int i = 1;
            foreach (var axles in countAxle)
            {
                if (axles.Value != 0)
                {
                    weighedperaxleList.Add(new weighedperaxle
                    {
                        itemNo = i,
                        axleConfig = axles.Key,
                        divertedPerAxle = axles.Value,
                        overloadedPerAxle = 0,
                        chargedPerAxle = 0,

                    });
                    i++;
                }
            }
            return weighedperaxleList.ToArray();

        }

        [HttpGet("report/weighedunderthreshold")]

        public Array axlethreshhold(DateTime fromdate, DateTime todate, String station, int threshold)
        {

            List<weighedunderthrehshold> weighedthresholds = new List<weighedunderthrehshold>();

            IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            var resultsData = query.Where(y => y.wbrg_ticket_date >= fromdate && y.wbrg_ticket_date <= todate).Where(z => z.wbrg_ticket_axleload > threshold).Select(x => new
            {
                x.id,
                x.veh_reg,
                x.wbrg_ticket_axel,
                x.wbrg_tx_code,
                x.wbrg_ticket_axleload,
                x.wbrg_ticket_gvwload

            }).ToList();
            int i = 1;
            foreach (var results in resultsData)
            {

                weighedthresholds.Add(new weighedunderthrehshold
                {
                    itemNo = i,
                    axleConfiguration = results.wbrg_ticket_axel.ToString(),
                    veh_reg = results.veh_reg.ToString(),
                    transporter = results.wbrg_tx_code.ToString(),
                    excessAxle = results.wbrg_ticket_axleload.ToString(),
                    gvwLoad = results.wbrg_ticket_gvwload.ToString(),

                }
               );
                i++;
            }

            return weighedthresholds.ToArray();
        }

        [HttpGet("report/habitualOffenders")]

        public Array habitualOffenders(DateTime fromdate, DateTime todate)
        {
            List<habitualOffenders> habitual = new List<habitualOffenders>();
            int i = 1;

            foreach (var line in _context.CaseDetails.Where(y => y.casedatetime >= fromdate && y.casedatetime <= todate).GroupBy(info => info.transportername)
                         .Select(group => new {
                             Transportername = group.Key,
                             Count = group.Count(),
                         })    
                         )
            {
                habitual.Add(new habitualOffenders
                {
             
                    transporterName = line.Transportername,
                    overLoaded = line.Count
                    
                });

              //cleared is not in the yardlist but has been cleared

              //charged is one who has the charge sheet

               // Console.WriteLine("{0} {1}", line.Transportername, line.Count);
            }
            var caseDocuments = _context.CaseDocs.Join(_context.CaseDetails, cd => cd.caseid, cr => cr.caseid, (cd, cr)=>
                new
                {
                    transportername = cr.transportername,
                    casenoplate = cr.caseregno,
                    caseid  = cr.caseid,
                    casedatetime = cr.casedatetime,
                    docname =cd.docname,
                }
            ).Where(y => y.casedatetime >= fromdate && y.casedatetime <= todate);

            var yardlist = _context.YardList.Join(_context.CaseDetails, yd => yd.vehreg, cr => cr.caseregno, (yd, cr) =>
               new
               {
                   vehreg = cr.caseregno,

               }
            );
            IQueryable<CaseDetails> query = _context.CaseDetails;
            var resultsData = query.Where(y => y.casedatetime >= fromdate && y.casedatetime <= todate).Select(x => new
            {
                x.id,
                x.transporteradd,
                x.transportername

            }).ToList();//add transporter address

            List<habitualOffenders> SortedList = habitual.OrderByDescending(o => o.overLoaded).ToList();
            foreach(var habitualoff in SortedList)
            {
                habitualoff.itemNo = i;
                habitualoff.transporteradd= resultsData.Where(y => y.transportername == habitualoff.transporterName).FirstOrDefault().transporteradd;
                habitualoff.charged = caseDocuments.Where(z => z.transportername == habitualoff.transporterName).Where(m=>m.docname=="Charge Sheet").Count();
                habitualoff.cleared = caseDocuments.Join(yardlist, cds => cds.casenoplate, yz => yz.vehreg, (cds, yz) => new {
                    transportername = cds.transportername,
                    docname = cds.docname,
                    vehreg = yz.vehreg
                }).Where(n => n.transportername == habitualoff.transporterName).Where(t=>t.vehreg==null || t.vehreg == "").Where(p => p.docname == "Load Correction Memo").Count();
                i++;
            }
            return SortedList.ToArray();
        }
    }
}
