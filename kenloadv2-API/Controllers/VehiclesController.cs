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
    public class VehiclesController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public VehiclesController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicles>>> GetVehicles(int offset, int limit = 50)
        {
            return await _context.Vehicles.Skip(offset).Take(limit).ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Vehicles>> GetVehicles(int id)
        {
            var Vehicles = await _context.Vehicles.FindAsync(id);

            if (Vehicles == null)
            {
                return NotFound();
            }

            return Vehicles;
        }

        [HttpGet("{vehicle_reg}")]
        public async Task<ActionResult<IEnumerable<Vehicles>>> GetVehicles(String vehicle_reg)
        {
            var ylist = await _context.Vehicles.Where(x => x.vehicle_reg == vehicle_reg).ToListAsync();

            if (ylist == null)
            {
                return NotFound();
            }

            return ylist;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Vehicles>>> GetVehiclesSearch(String vehicle_reg, String vehicle_trailer, String vehicle_axle, String vehicle_make, int offset, int limit = 50)
        {

            if (vehicle_trailer != null && vehicle_reg != null && vehicle_axle != null)
            {
                var Selections = await _context.Vehicles.Where(x => x.vehicle_reg == vehicle_reg && x.vehicle_axle == vehicle_axle && x.vehicle_trailer.Substring(0, vehicle_trailer.Length) != vehicle_trailer).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (vehicle_axle != null && vehicle_reg != null)
            {
                var Selections = await _context.Vehicles.Where(x => x.vehicle_reg == vehicle_reg && x.vehicle_axle == vehicle_axle).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }

            if (vehicle_reg != null)
            {
                var Selections = await _context.Vehicles.Where(x => x.vehicle_reg == vehicle_reg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (vehicle_axle != null)
            {
                var Selections = await _context.Vehicles.Where(x => x.vehicle_axle.Substring(0, vehicle_axle.Length) == vehicle_axle).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            if (vehicle_make != null)
            {
                var Selections = await _context.Vehicles.Where(x => x.vehicle_make == vehicle_make).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                return Selections;
            }
            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await _context.Vehicles.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;

        }
        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicles(int id, Vehicles Vehicles)
        {
            if (id != Vehicles.id)
            {
                return BadRequest();
            }

            _context.Entry(Vehicles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiclesExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicles>> PostVehicles(Vehicles Vehicles)
        {
            _context.Vehicles.Add(Vehicles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicles", new { id = Vehicles.id }, Vehicles);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicles(int id)
        {
            var Vehicles = await _context.Vehicles.FindAsync(id);
            if (Vehicles == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(Vehicles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiclesExists(int id)
        {
            return _context.Vehicles.Any(e => e.id == id);
        }
    }
}
