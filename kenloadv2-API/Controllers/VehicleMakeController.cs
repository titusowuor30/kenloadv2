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
    public class VehicleMakeController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public VehicleMakeController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/VehicleMake
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMake>>> GetVehicleMake()
        {
            return await _context.VehicleMake.ToListAsync();
        }

        // GET: api/VehicleMake/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMake>> GetVehicleMake(int id)
        {
            var vehicleMake = await _context.VehicleMake.FindAsync(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            return vehicleMake;
        }

        // PUT: api/VehicleMake/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleMake(int id, VehicleMake vehicleMake)
        {
            if (id != vehicleMake.id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleMake).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleMakeExists(id))
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

        // POST: api/VehicleMake
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleMake>> PostVehicleMake(VehicleMake vehicleMake)
        {
            _context.VehicleMake.Add(vehicleMake);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleMake", new { id = vehicleMake.id }, vehicleMake);
        }

        // DELETE: api/VehicleMake/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMake(int id)
        {
            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            _context.VehicleMake.Remove(vehicleMake);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleMakeExists(int id)
        {
            return _context.VehicleMake.Any(e => e.id == id);
        }
    }
}
