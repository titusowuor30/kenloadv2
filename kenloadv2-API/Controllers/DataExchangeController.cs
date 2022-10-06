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


namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]


    public class DataExchangeController : ControllerBase
    {
        private readonly Kenloadv2Data _context;
       

        public DataExchangeController(Kenloadv2Data context )
        {
            _context = context;
            
           
        }

        /*[HttpGet("/ReccuringJob")]
        public ActionResult CreateReccuringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _DataExchange.DataExchange(), Cron.Minutely);
            return Ok();
        }*/

        // GET: api/Camera
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataExchange>>> GetDataExchange()
        {
           
            return await _context.DataExchange.ToListAsync();
        }

        // GET: api/Camera/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataExchange>> GetDataExchange(int id)
        {
            var DataExchange = await _context.DataExchange.FindAsync(id);

            if (DataExchange == null)
            {
                return NotFound();
            }

            return DataExchange;
        }

        // PUT: api/Screens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> DataExchange(int id, DataExchange DataExchange)
        {
            if (id != DataExchange.id)
            {
                return BadRequest();
            }

            _context.Entry(DataExchange).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!DataExchangeExists(id))
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

        // POST: api/Camera
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataExchange>> PostDataExchange(DataExchange DataExchange)
        {
            _context.DataExchange.Add(DataExchange);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataExchange", new { id = DataExchange.id }, DataExchange);
        }

        // DELETE: api/DataExchange/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataExchange(int id)
        {
            var DataExchange = await _context.DataExchange.FindAsync(id);
            if (DataExchange == null)
            {
                return NotFound();
            }

            _context.DataExchange.Remove(DataExchange);
            await _context.SaveChangesAsync();

            return NoContent();
        }
       /*[HttpPost]
        public JobTestController(IDataExchange jobTestService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }*/
        private bool DataExchangeExists(int id)
        {
            return _context.DataExchange.Any(e => e.id == id);
}
    }
}