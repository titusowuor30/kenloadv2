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
    public class CameraController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public CameraController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/Camera
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Camera>>> GetCamera()
        {
            return await _context.Camera.ToListAsync();
        }

        // GET: api/Camera/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Camera>> GetCamera(int id)
        {
            var Camera = await _context.Camera.FindAsync(id);

            if (Camera == null)
            {
                return NotFound();
            }

            return Camera;
        }

        // PUT: api/Screens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamera(int id, Camera Camera)
        {
            if (id != Camera.id)
            {
                return BadRequest();
            }

            _context.Entry(Camera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CameraExists(id))
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
        public async Task<ActionResult<Camera>> PostCamera(Camera Camera)
        {
            _context.Camera.Add(Camera);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCamera", new { id = Camera.id }, Camera);
        }

        // DELETE: api/Camera/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamera(int id)
        {
            var Camera = await _context.Camera.FindAsync(id);
            if (Camera == null)
            {
                return NotFound();
            }

            _context.Camera.Remove(Camera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CameraExists(int id)
        {
            return _context.Camera.Any(e => e.id == id);
        }
    }
}
