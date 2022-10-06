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
    public class TagsExchangeController : ControllerBase
    {
        private readonly Kenloadv2Data _context;

        public TagsExchangeController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/TagsExchange
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagsExchange>>> GetTagsExchange()
        {
            return await _context.TagsExchange.ToListAsync();
        }

        // GET: api/TagsExchange/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagsExchange>> GetTagsExchange(int id)
        {
            var TagsExchange = await _context.TagsExchange.FindAsync(id);

            if (TagsExchange == null)
            {
                return NotFound();
            }

            return TagsExchange;
        }

        // GET: api/TagsExchange/5
        [HttpGet("getstatustag")]
        public ActionResult<TagsExchange> GetlocalTagsAsync()//get a tag that hasnt been synced where status equals 0;
        {

            TagsExchange resultsData = _context.TagsExchange.Where(t => t.status == 0).FirstOrDefault();

            if (resultsData == null)
            {
                return NotFound();
            }

            return resultsData;
        }

        [HttpGet("getstationtag")]
        public ActionResult<TagsExchange> getstationtag(string station, int status)//get a tag that hasnt been synced where status equals 0;
        {

            TagsExchange resultsData = _context.TagsExchange.Where(t => t.station == station && t.status==status).FirstOrDefault();

            if (resultsData == null)
            {
                return NotFound();
            }

            return resultsData;
        }


        // PUT: api/TagsExchange/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTagsExchange(int id, TagsExchange TagsExchange)
        {
            if (id != TagsExchange.id)
            {
                return BadRequest();
            }

            _context.Entry(TagsExchange).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagsExchangeExists(id))
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

        // POST: api/TagsExchange
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagsExchange>> PostTagsExchange(TagsExchange TagsExchange)
        {
            _context.TagsExchange.Add(TagsExchange);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTagsExchange", new { id = TagsExchange.id }, TagsExchange);
        }

        // DELETE: api/TagsExchange/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTagsExchange(int id)
        {
            var TagsExchange = await _context.TagsExchange.FindAsync(id);
            if (TagsExchange == null)
            {
                return NotFound();
            }

            _context.TagsExchange.Remove(TagsExchange);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagsExchangeExists(int id)
        {
            return _context.TagsExchange.Any(e => e.id == id);
        }
    }
}
