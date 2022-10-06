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
using Sharp7;
using System.Diagnostics;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IOSettingsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public IOSettingsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/IOSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IOSettings>>> GetIOSettings()
        {
            return await _context.IOSettings.ToListAsync();
        }

        // GET: api/IOSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IOSettings>> GetIOSettings(int id)
        {
            var IOSettings = await _context.IOSettings.FindAsync(id);

            if (IOSettings == null)
            {
                return NotFound();
            }

            return IOSettings;
        }

        [HttpPost("IOTest")]
        public string IOTest(String Action)
        {
          
            if (Action == "Open")
            {
                var client = new S7Client();
                int result = client.ConnectTo("192.168.0.8", 0, 1);
                if (result == 0)
                {
                    //Console.WriteLine("Connected");
                    Action = "Connected";
                    byte[] buf = new byte[1];
                    //byte lo = 0;
                    buf[0] = 0;
                    client.DBWrite(1, 1, 1, buf);//NI1 /HI          

                }
                else
                {
                    Action = client.ErrorText(result) + "=>For Open"; ;
                }
                return Action;
            }
            if (Action == "Close")
            {
                var client = new S7Client();
                int result = client.ConnectTo("192.168.0.8", 0, 1);
                if (result == 0)
                {
                    //Console.WriteLine("Connected");
                    Action = "Connected";
                    byte[] buf = new byte[1];
                    //byte lo = 0;
                    buf[0] = 0;
                    client.DBWrite(1, 2, 1, buf);//NI1 /HI          

                }
                else
                {
                    Action = client.ErrorText(result) + "=>For Close"; ;
                }
                return Action;
            }
            return "IO configs NULL for Entry IO";
        }
        [HttpPost("deckEntry")]
        public string deckEntry(String Action)
        {
            var IOSettings = _context.IOSettings.Where(z => z.deckEntry == true).FirstOrDefault();

            if (Action == "Open" && IOSettings != null)
            {
                var client = new S7Client();
                int result = client.ConnectTo(IOSettings.IPAddress, 0, 1);
                if (result == 0)
                {
                    //Console.WriteLine("Connected");
                    Action = "Connected";
                    byte[] buf = new byte[1];
                    byte[] buf2 = new byte[1];
                    //byte lo = 0;
                    buf[0] = 1;
                    buf2[0] = 0;
                    client.DBWrite(1, 1, 1, buf);//NI1 /HI
                    client.DBWrite(1, 1, 1, buf2);//NI1 /HI  

                }
                else
                {
                    Action = client.ErrorText(result) + "=>For Open"; ;
                }
                return Action;
            }
            return "IO configs NULL for Entry IO";
        }
        [HttpPost("deckExit")]
        public string deckExit(String Action)
        {
            var IOSettings = _context.IOSettings.Where(z => z.deckExit == true).FirstOrDefault();

            if (Action == "Legal" && IOSettings != null)
            {
                var client = new S7Client();
                int result = client.ConnectTo(IOSettings.IPAddress, 0, 1);
                if (result == 0)
                {
                    //Console.WriteLine("Connected");
                    Action = "Connected";
                    byte[] buf = new byte[1];
                    byte[] buf2 = new byte[1];
                    //byte lo = 0;
                    buf[0] = 1;
                    buf2[0] = 0;

                    client.DBWrite(1, 2, 1, buf);//NI1 /HI
                    client.DBWrite(1, 2, 1, buf2);//NI1 /HI

                }
                else
                {
                    Action = client.ErrorText(result)+"=>For Legal";
                }
                return Action;
            }
            if (Action == "Yard" && IOSettings != null)
            {
                var client = new S7Client();
                int result = client.ConnectTo(IOSettings.IPAddress, 0, 1);
                if (result == 0)
                {
                    //Console.WriteLine("Connected");
                    Action = "Connected";
                    byte[] buf = new byte[1];
                    byte[] buf2 = new byte[1];
                    //byte lo = 0;
                    buf[0] = 1;
                    buf2[0] =0;
                    client.DBWrite(1, 3, 1, buf);//NI1 /HI
                    client.DBWrite(1, 3, 1, buf2);//NI1 /HI


                }
                else
                {
                    Action = client.ErrorText(result) + "=>For Yard"; ;
                }
                return Action;
            }
         

            return "IO configs NULL for Exit IO";
        }
        [HttpPost("reset")]
        public string deckreset(String Action)
        {
            var IOSettings = _context.IOSettings.Where(z => z.deckEntry == true).FirstOrDefault();

            if (Action == "Reset" && IOSettings != null)
            {
                Action = actionIO("Error", IOSettings.IPAddress, "Reset-" + Action, 7);

            }
            else
            {
                Action = "deckEntry IO Configs Null";
                return Action;
            }
            return Action;
        }

        [HttpPost("left")]
        public string leftdeck(String Action)
        {
            var IOSettings = _context.IOSettings.Where(z => z.deckEntry == true).FirstOrDefault();

            if (Action == "Legal" && IOSettings != null)
            {
               Action=  actionIO("Error", IOSettings.IPAddress, "Left-" + Action, 1);

            }
            else if (Action == "Yard" && IOSettings != null)
            {
                Action = actionIO("Error", IOSettings.IPAddress, "Left-" + Action, 2);
            }
            else if (Action == "Open" && IOSettings != null)
            {
                Action = actionIO("Error", IOSettings.IPAddress, "Left Open-" + Action, 5);
            }
            else
            {
                Action = "Left IO Configs Null";
                return Action;
            }
            return Action;
        }

        [HttpPost("Right")]
        public string rightdeck(String Action)
        {
            var IOSettings = _context.IOSettings.Where(z => z.deckExit == true).FirstOrDefault();

            if (Action == "Legal" && IOSettings != null)
            {
               Action= actionIO("Error", IOSettings.IPAddress, "Right-" + Action, 3);
            }
            else if (Action == "Yard" && IOSettings != null)
            {
               Action= actionIO("Error", IOSettings.IPAddress, "Right-" + Action, 4);
            }
            else if (Action == "Open" && IOSettings != null)
            {
                Action = actionIO("Error", IOSettings.IPAddress, "Right Open-" + Action, 6);
            }
            else
            {
                Action = "Right IO Configs Null";
                return Action;
            }
            return Action;
        }


        // PUT: api/IOSettings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIOSettings(int id, IOSettings IOSettings)
        {
            if (id != IOSettings.id)
            {
                return BadRequest();
            }

            _context.Entry(IOSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IOSettingsExists(id))
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

        // POST: api/IOSettings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IOSettings>> PostIOSettings(IOSettings IOSettings)
        {
            _context.IOSettings.Add(IOSettings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIOSettings", new { id = IOSettings.id }, IOSettings);
        }

        // DELETE: api/IOSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIOSettings(int id)
        {
            var IOSettings = await _context.IOSettings.FindAsync(id);
            if (IOSettings == null)
            {
                return NotFound();
            }

            _context.IOSettings.Remove(IOSettings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string actionIO(string Action, string IPAddress,string IODirection, int signal)
        {
            var client = new S7Client();
            int result = client.ConnectTo(IPAddress, 0, 1);
            if (result == 0)
            {
                //Console.WriteLine("Connected");
                Action = "Connected";
                byte[] buf = new byte[1];
                byte[] buf2 = new byte[1];
                //byte lo = 0;
                buf[0] = 1;
                buf2[0] = 0;
                client.DBWrite(1, signal, 1, buf);//NI1 /HI
                client.DBWrite(1, signal, 1, buf2);//NI1 /HI


            }
            else
            {
                Action = client.ErrorText(result) + IODirection ;
            }
            return Action;
        }
        private bool IOSettingsExists(int id)
        {
            return _context.IOSettings.Any(e => e.id == id);
        }
    }
}
