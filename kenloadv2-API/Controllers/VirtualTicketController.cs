using System;
using System.IO;
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
using System.Web;

namespace KenloadV2API.Controllers
{
    class wimweighedperaxle
    {
        public int itemNo { get; set; }
        public string axleConfig { get; set; }
        public int weighedPerAxle { get; set; }
        public int calledinPerAxle { get; set; }

    }

    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VirtualTicketsController : ControllerBase
    {
        private const string V = "000000";
        private readonly Kenloadv2Data _context;

        public VirtualTicketsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/VirtualTicket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VirtualTicket>>> GetVirtualTickets()
        {
            return await _context.VirtualTicket.OrderByDescending(a => a.id).ToListAsync();
        }

        // GET: api/VirtualTicket/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VirtualTicket>> GetVirtualTickets(int id)
        {
            var VirtualTicket = await _context.VirtualTicket.FindAsync(id);

            if (VirtualTicket == null)
            {
                return NotFound();
            }

            return VirtualTicket;
        }
       
 

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<VirtualTicket>>> GetVirtualTicketsSearch(String veh_reg, String tag, String trucks, String station, String wbrg_ticket_no, String wbrg_ticket_no_update, String wim, String asc, DateTime fromdate, DateTime todate, int exported, int offset, int calledin =-1,  int limit =50)
        {
            IQueryable<VirtualTicket> query = _context.VirtualTicket;
            var predicate = PredicateBuilder.False<VirtualTicket>();

          
            

            if (exported >=0)
            {
                
                    query = query.Where(x => x.exported == exported);
               
            }

            if (calledin >=0)
            {
               
               
                    query = query.Where(x => x.calledin == calledin);
                
            }


            

            if (wim != null)
            {
                
                if (wim.Contains(","))
                {
                    var sval = wim.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.wim.Contains(val) );
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wim.Contains(wim));
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
                        predicate = predicate.Or(x => x.wim.Substring(0, val.Length) == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wim.Substring(0, station.Length) == station);
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
                                predicate = predicate.Or(x => x.lp.Substring(0, val.Length) == val);
                                predicate = predicate.Or(x => x.anprassist.Substring(0, val.Length) == val);
                                predicate = predicate.Or(x => x.lp_back.Substring(0, val.Length) == val);
                                id++;
                            }

                            query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.lp.Substring(0, veh_reg.Length) == veh_reg);
                    predicate = predicate.Or(x => x.anprassist.Substring(0, veh_reg.Length) == veh_reg);
                    predicate = predicate.Or(x => x.lp_back.Substring(0, veh_reg.Length) == veh_reg);
                }
            }

            //if (veh_reg != null)
            //{
            //    if (veh_reg.Contains(","))
            //    {
            //        var sval = veh_reg.Split(",");

            //        int clen = sval.Length;
            //        int id = 0;
            //        for (int g = 0; g < clen; g++)
            //        {
            //            String val = sval[id];
            //            predicate = predicate.Or(x => x.anprassist.Substring(0, val.Length) == val);
            //            id++;
            //        }

            //        query = query.Where(predicate);
            //    }
            //    else
            //    {
            //        query = query.Where(x => x.anprassist.Substring(0, veh_reg.Length) == veh_reg);
            //    }
            //}
            //if (veh_reg != null)
            //{
            //    if (veh_reg.Contains(","))
            //    {
            //        var sval = veh_reg.Split(",");

            //        int clen = sval.Length;
            //        int id = 0;
            //        for (int g = 0; g < clen; g++)
            //        {
            //            String val = sval[id];
            //            predicate = predicate.Or(x => x.lp.Substring(0, val.Length) == val);
            //            id++;
            //        }

            //        query = query.Where(predicate);
            //    }
            //    else
            //    {
            //        query = query.Where(x => x.lp.Substring(0, veh_reg.Length) == veh_reg);
            //    }
            //}
            if (wbrg_ticket_no != null)
            {
                if (wbrg_ticket_no.Contains(","))
                {
                    var sval = wbrg_ticket_no.Split(",");

                    int clen = sval.Length;
                    int id = 0;
                    for (int g = 0; g < clen; g++)
                    {
                        String val = sval[id];
                        predicate = predicate.Or(x => x.wbrg_ticket_no.Substring(0, val.Length) == val);
                        id++;
                    }

                    query = query.Where(predicate);
                }
                else
                {
                    query = query.Where(x => x.wbrg_ticket_no.Substring(0, wbrg_ticket_no.Length) == wbrg_ticket_no);
                }
                
            }
            if (wbrg_ticket_no_update != null)
            {
                
                    query = query.Where(x => x.wbrg_ticket_no == "" || x.wbrg_ticket_no == null);
               

            }

            if (trucks != null)
            {

                query = query.Where(x => x.vehicle_class >=5 && x.vehicle_class != 30);


            }

            if (fromdate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && todate != DateTime.Parse("01/Jan/0001 12:00:00 AM") && veh_reg == null)
            {
                query = query.Where(x => x.date_time_local >= fromdate && x.date_time_local <= todate);
            }

           

            if (asc != null)
            {
                //query = query.OrderByDescending(a => a.date_time_local);
            }
            else
            {
                query = query.OrderByDescending(a => a.id);
            }
            if (asc == "cid")
            {
                query = query.OrderBy(a => a.cid);
            }
            if (asc == "ciddesc")
            {
                query = query.OrderByDescending(a => a.cid);
            }
            if (asc == "date")
            {
                query = query.OrderByDescending(a => a.date_time_local);
            }
            query = query.Skip(offset).Take(limit);
            var Selection2 = query.ToArray();
            if (tag != null)
            {
                IQueryable<Tag> tags = _context.Tag;
              
                for (int g = 0; g < Selection2.Length; g++)
                {
                    Selection2[g].tagged = 0;
                    
                    
                    if (Selection2[g].lp != null && !Selection2[g].lp.Contains("UNKNOWN") && Selection2[g].lp != "" )
                    {
                        var lp = Selection2[g].lp;
                        var tagsQ = tags.Where(x => x.vehreg.Trim() == Selection2[g].lp && x.status == 0).ToArray();
                        var len = tagsQ.Length;
                        foreach (var weighstatic in tagsQ)
                        {
                            
                            Selection2[g].tagged = 1;
                            
                        }

                    }
                }
            }
            var Selection = Selection2.ToList(); ;
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;
            
        }

        [HttpGet("{wbrg_ticket_no}")]
        public async Task<ActionResult<VirtualTicket>> GetVirtualTickets(String wbrg_ticket_no)
        {
            var ylist = await _context.VirtualTicket.Where(x => x.wbrg_ticket_no == wbrg_ticket_no).FirstOrDefaultAsync();

            if (ylist == null)
            {
                //return NotFound();
            }

            return ylist;
        }

        // PUT: api/VirtualTicket/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVirtualTickets(int id, VirtualTicket VirtualTicket)
        {
            if (id != VirtualTicket.id)
            {
                return BadRequest();
            }

            _context.Entry(VirtualTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VirtualTicketsExists(id))
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

        // POST: api/VirtualTicket
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VirtualTicket>> PostVirtualTickets(VirtualTicket VirtualTicket)
        {
            var lp_image_front = VirtualTicket.lp_image_front;
            var lp_image_back = VirtualTicket.lp_image_back;
            var overview = VirtualTicket.overview;

            VirtualTicket.overview = "";
            VirtualTicket.lp_image_back = "";
            VirtualTicket.lp_image_front = "";

            _context.VirtualTicket.Add(VirtualTicket);
            await _context.SaveChangesAsync();



            var hour = VirtualTicket.date_time_local.Hour.ToString();
            var year = VirtualTicket.date_time_local.Year.ToString();
            var month = VirtualTicket.date_time_local.Month.ToString();
            if (month.Length < 2) { month = "0" + month; }
            if (hour.Length < 2) { hour = "0" + hour; }
            var day = VirtualTicket.date_time_local.Day.ToString();
            if (day.Length < 2) { day = "0" + day; }
            var dir = year + month + "/" + day + "/" + hour + "/";
            string imageFolderPath = "C:\\kenloadimg\\";
            //var filePath = imageFolderPath + dir + "O" + VirtualTicket.cid + ".jpg";
            var folderPath = HttpUtility.UrlDecode(imageFolderPath + dir);
            string diry = @"" + folderPath;
            // If directory does not exist, create it
            if (!Directory.Exists(diry))
            {
                Directory.CreateDirectory(diry);
            }

            //using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderPath, "L" + VirtualTicket.cid + ".jpg")))
            //{
            //    byte[] imageBytes = Convert.FromBase64String(overview.ToString());
            //    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            //    ms.Write(imageBytes, 0, imageBytes.Length);

            //    outputFile.WriteLine(overview.ToString());
            //}

            //using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderPath, "F" + VirtualTicket.cid + ".jpg")))
            //{
            if (lp_image_back.Length > 500)
            {
                try { 
                byte[] decodedByteArray =
           Convert.FromBase64CharArray(overview.ToCharArray(),
                                         0, lp_image_back.Length);
                FileStream fstrm = new FileStream(@"" + folderPath + "LB" + VirtualTicket.cid + ".jpg", FileMode.CreateNew, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(fstrm);

                writer.Write(decodedByteArray);
                writer.Close();
                fstrm.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } 
            if (lp_image_front.Length > 500)
            {
                try
                {
               
                byte[] decodedByteArray =
           Convert.FromBase64CharArray(lp_image_front.ToCharArray(),
                                         0, lp_image_front.Length);
                FileStream fstrm = new FileStream(@"" + folderPath + "LF" + VirtualTicket.cid + ".jpg", FileMode.CreateNew, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(fstrm);

                writer.Write(decodedByteArray);
                writer.Close();
                fstrm.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            if (overview.Length > 500)
            {
                try
                {

                
                byte[] decodedByteArray =
           Convert.FromBase64CharArray(overview.ToCharArray(),
                                         0, overview.Length);
                FileStream fstrm = new FileStream(@"" + folderPath + "O" + VirtualTicket.cid + ".jpg", FileMode.CreateNew, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(fstrm);

                writer.Write(decodedByteArray);
                writer.Close();
                fstrm.Close();
            }
                catch (Exception e)
            {
                    Console.WriteLine(e);
                }
        }


            // outputFile.WriteLine(imageBytes);
            //}

            //using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderPath, "R" + VirtualTicket.cid + ".jpg")))
            //{

            //    byte[] imageBytes = Convert.FromBase64String(lp_image_back.ToString());
            //    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            //    ms.Write(imageBytes, 0, imageBytes.Length);

            //    outputFile.WriteLine(imageBytes);
            //}


            return CreatedAtAction("GetVirtualTickets", new { id = VirtualTicket.id }, VirtualTicket);
        }
        private bool VirtualTicketsExists(int id)
        {
            return _context.VirtualTicket.Any(e => e.id == id);
        }

        // DELETE: api/VirtualTicket/5

        [HttpGet("report/vehicleweighedperaxle")]

        public Array Weighedperaxle(DateTime fromdate, DateTime todate, String station)
        {
            List<wimweighedperaxle> weighedperaxleList = new List<wimweighedperaxle>();
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
            IQueryable<VirtualTicket> query = _context.VirtualTicket;
            var resultsData = query.Where(y => y.date_time_local >= fromdate && y.date_time_local <= todate).Where(z => !string.IsNullOrEmpty(z.axle_conf)).Select(x => new
            {
                x.id,
                x.axle_conf,

            }).ToList();
            foreach (var transaction in resultsData)
            {
                string axleType = "";
                try
                {


                    axleType = transaction.axle_conf.Substring(0, 2);

                    if (countAxle.ContainsKey(axleType))
                    {
                        countAxle[axleType] += 1;
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            int i = 1;
            foreach (var axles in countAxle)
            {
                if (axles.Value != 0)
                {
                    weighedperaxleList.Add(new wimweighedperaxle
                    {
                        itemNo = i,
                        axleConfig = axles.Key,
                        weighedPerAxle = axles.Value,
                        calledinPerAxle = 0,

                    });
                    i++;
                }
            }
            return weighedperaxleList.ToArray();

        }

    }
}
