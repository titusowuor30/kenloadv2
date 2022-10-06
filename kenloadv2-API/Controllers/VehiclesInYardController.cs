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
    public class VehiclesInYardController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public VehiclesInYardController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/VehiclesInYard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiclesInYard>>> GetVehiclesInYard()
        {
            return await _context.VehiclesInYard.ToListAsync();
        }

        // GET: api/VehiclesInYard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehiclesInYard>> GetVehiclesInYard(int id)
        {
            var vehiclesInYard = await _context.VehiclesInYard.FindAsync(id);

            if (vehiclesInYard == null)
            {
                return NotFound();
            }

            return vehiclesInYard;
        }

        // PUT: api/VehiclesInYard/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiclesInYard(int id, VehiclesInYard vehiclesInYard)
        {
            if (id != vehiclesInYard.id)
            {
                return BadRequest();
            }

            _context.Entry(vehiclesInYard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiclesInYardExists(id))
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

        // POST: api/VehiclesInYard
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehiclesInYard>> PostVehiclesInYard(VehiclesInYard vehiclesInYard)
        {
            _context.VehiclesInYard.Add(vehiclesInYard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehiclesInYard", new { id = vehiclesInYard.id }, vehiclesInYard);
        }

        // DELETE: api/VehiclesInYard/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiclesInYard(int id)
        {
            var vehiclesInYard = await _context.VehiclesInYard.FindAsync(id);
            if (vehiclesInYard == null)
            {
                return NotFound();
            }

            _context.VehiclesInYard.Remove(vehiclesInYard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiclesInYardExists(int id)
        {
            return _context.VehiclesInYard.Any(e => e.id == id);
        }
    }
}
