using KenloadV2API.Data;
using KenloadV2API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PoliceReportsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public PoliceReportsController(Kenloadv2Data context)
        {
            _context=context;
        }

        [HttpGet("DailyHour")]

        public Array getDailyHourData(DateTime fromdate,DateTime todate,string station, string mode="A")
        {
            if (mode == "A")//if mode == A then load automatic data else load manual data from selected user dates
            {
                fromdate = DateTime.Now.Subtract(TimeSpan.FromHours(6));
                todate = DateTime.Now;

                DateTime currentTime = DateTime.Now;

                if (currentTime.Hour < 12 && currentTime.Hour >= 9)//system accessed during morning hours
                {
                    //morning hrs
                    fromdate = Convert.ToDateTime((DateTime.Now.Date.ToString("yyyy-MM-dd") + " 09:00 AM")).Subtract(TimeSpan.FromHours(18));//from 1500hrs - 0900hrs
                    todate = Convert.ToDateTime((DateTime.Now.Date.ToString("yyyy-MM-dd") + " 09:00 AM"));
                    Console.WriteLine("From Date:" + fromdate + " To Date:" + todate);
                }
                else if (currentTime.Hour >= 12 && currentTime.Hour < 20)
                {
                    //afternoon hrs
                    fromdate = Convert.ToDateTime((DateTime.Now.Date.ToString("yyyy-MM-dd") + " 09:00 AM"));//from 0900hrs to 1500hrs
                    todate = Convert.ToDateTime((DateTime.Now.Date.ToString("yyyy-MM-dd") + " 15:00 PM"));
                    Console.WriteLine("From Date:" + fromdate + " To Date:" + todate);
                }
            }
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
                x.wbrg_ticket_ref,// O OVERLOAD, L legal W warned
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
            var wideload=_context.WideLoad.Where(a=>a.DateofTravel >= fromdate && a.DateofTravel <= todate && a.status=="C").Select(x => new
            {
                x.id,
                x.status,
                x.DateofTravel,
            }).ToList();
            var detainedNew = _context.CaseDetails.Where(y=>y.weighdate>=fromdate && y.weighdate <=fromdate).Select(x => new
            {
                x.id,
                x.weighdate,
            });
            //afternoon hrs
            DateTime fromTime = todate;
            Console.WriteLine("From Date:" + fromdate + " To Date:" + todate);
            DateTime prevfromdate =fromdate;
            DateTime prevtodate = todate; 
            if (fromTime.Hour < 12 && fromTime.Hour >= 9)//system accessed during morning hours
            {
                //morning hrs
                prevfromdate = Convert.ToDateTime((DateTime.Now.Date.ToString("yyyy-MM-dd") + " 09:00 AM")).Subtract(TimeSpan.FromHours(18));//from 1500hrs - 0900hrs
                prevtodate = Convert.ToDateTime((DateTime.Now.Date.ToString("yyyy-MM-dd") + " 09:00 AM"));
                Console.WriteLine("From Date:" + prevfromdate + " To Date:" + prevtodate);
            }
            else if (fromTime.Hour >= 12 && fromTime.Hour < 20)
            {
                //afternoon hrs
                prevfromdate =fromdate;//from 1500hrs - 0900hrs
                prevtodate = todate;
                Console.WriteLine("From Date:" + prevfromdate + " To Date:" + prevtodate);
            }

            var detainedPrev = _context.CaseDetails.Where(y => y.weighdate >= prevfromdate && y.weighdate <= prevtodate).Select(x => new
            {
                x.id,
                x.weighdate,
            });
            var amountpaid = _context.CaseResults.Where(y => y.datetime >= fromdate && y.datetime <= todate).Select(x => new
            {
                x.id,
                x.drivercharge,
                x.datetime,
                x.loadercharge,
                x.ownercharge,
                x.parkingfee,
                x.transportercharge,
            }).ToList();
            var released = _context.YardList.Where(y => y.datetime >= fromdate && y.datetime <= todate).Select(x => new
            {
                x.id,
                x.datetime,
                x.spreleaseno,
            }).ToList();
            int rowid = 1;
            int countMultiDeck = 0;
            int totalMultideck = 0;
            int countoverload = 0;
            int totaloverload = 0;
            int countChargedEAC = 0;
            int countChargedTraffic = 0; 
            int totalChargedEAC = 0;
            int totalChargedTraffic = 0;
            int countwideloadEAC = 0;
            int totalwideloadEAC = 0;
            int countprevDetained=0;
            int countNewDetained=0;
            int totalPrevDetained = 0;
            int totalNewDetained= 0;
            int countCourtFine = 0;
            int totalCourtFine = 0;
            int countOverloadCharge = 0;
            int totalOverloadCharge = 0;
            int countReleased = 0;
            int totalReleased = 0;

            List<HourData> rowData = new List<HourData>();
            DateTime dateTracker = fromdate;
            //track 24 hours before repeats start// change todate to be tracker+24hours if todate is still later
            while (DateTime.Compare(dateTracker, todate) < 0)//iterates till the last hour
            {
                foreach (var recordData in resultsData)
                {
                    if (recordData.wbrg_ticket_type == "M" && string.IsNullOrEmpty(recordData.wbrg_ticket_ref))//hours within 24hours
                    {
                        //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                        countMultiDeck++;
                    }
                    
                }

                foreach (var charge in chargeData)
                {
                    if (charge.docname == "EAC Act")//hours within 24hours
                    {
                        //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                        countChargedEAC++;
                    }
                    if (charge.docname == "Traffic Act" )//hours within 24hours
                    {
                        //count and add Mutildeck where wbrg_ticket_ref is nullorEmpty
                        countChargedTraffic++;
                    }

                    //}
                }
                foreach (var wide in wideload)
                {
                    countwideloadEAC++;
                }
                foreach (var detained in detainedPrev)
                {
                   countprevDetained++; 
                }
                foreach (var detained in detainedPrev)
                {
                    countNewDetained++;
                }
                foreach (var amount in amountpaid)
                {
                    countCourtFine += (Convert.ToInt32(amount.drivercharge) + Convert.ToInt32(amount.ownercharge) +
                        Convert.ToInt32(amount.parkingfee) + Convert.ToInt32(amount.transportercharge));
                }
                     foreach (var amount in amountpaid)
                {
                    countOverloadCharge += (Convert.ToInt32(amount.loadercharge));
                }
                foreach (var v in released)
                {
                    if (v.spreleaseno != "")//hours within 24hours
                    {
                        countReleased++;
                    }
                }
                rowData.Add(new HourData
                {
                    id = rowid.ToString(),
                    WeighedStatic = countMultiDeck.ToString(),
                    overloadEAC = countChargedEAC.ToString(),
                    overloadTraffic=countChargedTraffic.ToString(),
                    overloadWideloadEAC=countwideloadEAC.ToString(),
                    overloadWideloadTraffic="0",
                    prevDetainedEAC = countprevDetained.ToString(),
                    prevDetainedTraffic = "0",
                    newDetainedEaC = countNewDetained.ToString(),
                    newDetainedTraffic = "0",
                    CourtFine = countCourtFine.ToString(),
                    OverloadCharge=countOverloadCharge.ToString(),
                    Released=countReleased.ToString(),
                });
                rowid++;
                totalChargedEAC = totalChargedEAC + countChargedEAC;
                countChargedEAC = 0;
                totalChargedTraffic=totalChargedTraffic + countChargedTraffic;
                countChargedTraffic = 0;
                totalMultideck = totalMultideck + countMultiDeck;
                countMultiDeck = 0;
                totaloverload = totaloverload + countoverload;
                countoverload = 0;
                totalwideloadEAC = totalwideloadEAC + countwideloadEAC;
                countwideloadEAC=0;
                totalNewDetained =totalNewDetained + countNewDetained;
                countNewDetained=0;
                totalPrevDetained = totalPrevDetained + countprevDetained;
                totalCourtFine=totalCourtFine + countCourtFine;
                countCourtFine = 0;
                totalOverloadCharge=totalOverloadCharge + countOverloadCharge;
                countOverloadCharge=0;
                totalReleased=totalReleased + countReleased;
                countReleased=0;
                dateTracker = dateTracker.AddHours(24);
                //create row
                //change Row for data
                //append total to array onve done
                //end while loop 
            }
            rowData.Add(new HourData
            {
                id = "TOTAL",
                //weighed
                WeighedStatic = totalMultideck.ToString(),
                WeighedMobile = "0",
                //chrged
                overloadEAC = totalChargedEAC.ToString(),
                overloadTraffic = totalChargedTraffic.ToString(),
                overloadWideloadEAC = totalwideloadEAC.ToString(),
                overloadWideloadTraffic = "0",
                //detained
                prevDetainedEAC = totalPrevDetained.ToString(),
                prevDetainedTraffic="0",
                newDetainedEaC = totalNewDetained.ToString(),
                newDetainedTraffic="0",
                //amount paid
                CourtFine = totalCourtFine.ToString(),
                OverloadCharge = totalOverloadCharge.ToString(),
                //released
                Released = totalReleased.ToString(),
                fromdate=fromdate.ToString(),
                todate=todate.ToString(),
            });
            return rowData.ToArray();
        }
    }

    public class HourData
    {
        public string id { get; set; }
        //weighed
        public string WeighedStatic { get; set; }
        public string WeighedMobile { get; set; }
        //charged
        public string overloadEAC { get; set; }
        public string overloadTraffic { get; set; }
        public string overloadWideloadEAC { get; set; }
        public string overloadWideloadTraffic { get; set; }
        //detained
        public string prevDetainedEAC { get; set; }
        public string prevDetainedTraffic { get; set; }
        public string newDetainedEaC { get; set; }  
        public string newDetainedTraffic { get; set; }  
        //amount paid
        public string CourtFine { get; set; }   
        public string OverloadCharge { get; set; }
        //released
        public string Released { get; set; }    
        //fetch TimeSpane
        public string fromdate { get; set; }
        public string todate { get; set; }  
    }
}
