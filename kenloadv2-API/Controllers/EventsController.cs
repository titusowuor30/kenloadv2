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
    public class EventsController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public EventsController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Events>>> GetEvents()
        {
            return await _context.Events.OrderByDescending(x=> x.id).ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Events>> GetEvents(int id)
        {
            var Events = await _context.Events.FindAsync(id);

            if (Events == null)
            {
                return NotFound();
            }

            return Events;
        }

        
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Events>>> GetEventsSearch(String events, int offset, int limit = 50)
        {

         
           
            if (events != null)
            {
                var Selections = await _context.Events.Where(x => x.events == events).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await _context.Events.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvents(int id, Events Events)
        {
            if (id != Events.id)
            {
                return BadRequest();
            }

            _context.Entry(Events).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Events>> PostEvents(Events Events)
        {
            _context.Events.Add(Events);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvents", new { id = Events.id }, Events);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvents(int id)
        {
            var Events = await _context.Events.FindAsync(id);
            if (Events == null)
            {
                return NotFound();
            }

            _context.Events.Remove(Events);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.id == id);
        }
    }
}
