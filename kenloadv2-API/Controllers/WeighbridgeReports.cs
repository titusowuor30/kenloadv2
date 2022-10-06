using KenloadV2API.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KenloadV2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeighbridgeReports : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public WeighbridgeReports(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/<WeighbridgeReports>
        [HttpGet("{DailyHourData}")]

        public string Get(String startDate="", String endDate="", String startHour="", String endHour="" )
        {
            /*var prStartDate = DateTime.Parse(startDate);
            var prEndDate = DateTime.Parse(endDate);
            //fnd difference in dates
            //call per hour and fill an oject
            var HswimNo = _context.Hswim.Where(x => x.timestamp <= prEndDate).Where(x=>x.timestamp >= prStartDate).Count();
            var HswimCalledIn = _context.Hswim.Where(x => x.timestamp <= prEndDate).Where(x => x.timestamp >= prStartDate).Count();
            var MultideckNo = _context.WeighbridgeTransactions.Where(x => x.wbrg_ticket_date <= prEndDate).Where(x => x.wbrg_ticket_date >= prStartDate).Count();
            var ManualNo = _context.WeighbridgeTransactions.Where(x => x.wbrg_ticket_date <= prEndDate).Where(x => x.wbrg_ticket_date >= prStartDate).Count();
            var TotalWeighed = _context.WeighbridgeTransactions.Where(x => x.wbrg_ticket_date <= prEndDate).Where(x => x.wbrg_ticket_date >= prStartDate).Count(); */

            //return object
            /* return HswimNo.ToString();*/
            return startDate + endDate;
        }

        // GET api/<WeighbridgeReports>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WeighbridgeReports>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WeighbridgeReports>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WeighbridgeReports>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
