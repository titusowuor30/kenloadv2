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
using Newtonsoft.Json;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EACActController : ControllerBase
    {
        private readonly Kenloadv2Data _context;
        private const string V = "000000";

        public EACActController(Kenloadv2Data context)
        {
            _context = context;
        }

        // GET: api/EACAct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EACAct>>> GetEACAct()
        {
            return await _context.EACAct.ToListAsync();
        }

       
        // GET: api/EACAct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EACAct>> GetEACAct(int id)
        {
            var EACAct = await _context.EACAct.FindAsync(id);

            if (EACAct == null)
            {
            
                return NotFound();
            }

            return EACAct;
        }
        // PUT: api/EACAct/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEACAct(int id, EACAct EACAct)
        {
            if (id != EACAct.id)
            {
                return BadRequest();
            }

            _context.Entry(EACAct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EACActExists(id))
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

        [HttpGet("EACActGVWCharges")]
        public String GetEACActGVWCharges(String period,  int GVWOverloadkg, int offset, int limit = 50, int overloadkg = -1, int post = -1)
        {
            IQueryable<GVWoverloadCharges> querygvw = _context.GVWoverloadCharges;
            double gvwfee = 0;

            if (GVWOverloadkg > 0)
            {
                if (GVWOverloadkg > 31500)
                {
                    GVWOverloadkg = 31500;
                }
                querygvw = querygvw.Where(x => x.period.Substring(0, period.Length) == period);
                querygvw = querygvw.Where(x => x.overloadkg >= GVWOverloadkg );
                var gvwfees2 = querygvw.OrderBy(a => a.overloadkg).ToList();
                var gvwfees = querygvw.OrderBy(a => a.overloadkg).Skip(0).Take(1).ToList();
                gvwfee= gvwfees[0].fee;
            }
            return gvwfee + "";
}

[HttpGet("charges")]
        public String GetEACActCharges(String period,String caseid, String vehreg, String act, int GVWOverloadkg, int steeringaxle, int singledriveaxle, int tandemaxle, int tridemaxle, int offset, int limit = 50, int overloadkg = -1, int post = -1)
        {
            IQueryable<GVWoverloadCharges> querygvw = _context.GVWoverloadCharges;
            IQueryable<AVWoverloadCharges> queryavw = _context.AVWoverloadCharges;
            IQueryable<AVWdemeritPoints> queryavwdemerit = _context.AVWdemeritPoints;
            IQueryable<GVWdemeritPoints> querygvwdemerit = _context.GVWdemeritPoints;
            IQueryable<Vehicles> queryvehreg = _context.Vehicles;
            IQueryable<Penalties> queryPenalties = _context.Penalties;
            IQueryable<DollarRate> queryDollar = _context.DollarRate;


           

            int vehicle_demeritpoints = 1;
            int totalpoints = 1;
            double gvwfee = 0;
            int gvwpoints_ = 1;
            double singledriveaxlefee_ = 0;
            double steeringaxlefee_ = 0;
            double tandemaxlefee_ = 0;
            double tridemaxlefee_ = 0;

            int steeringaxlepoint_ = 0;
            int singledriveaxlepoint_ = 0;
            int tandemaxlepoint_ = 0;
            int tridemaxlepoint_ = 0;

          

            if (GVWOverloadkg > 0)
            {
                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                if (GVWOverloadkg > 31500)
                {
                    GVWOverloadkg = 31500;
                }
                querygvw = querygvw.Where(x => x.period.Substring(0, period.Length) == period);

                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                querygvw = querygvw.Where(x => x.overloadkg >= GVWOverloadkg );
                //querygvw.Skip(0).Take(1).ToList();
                var gvwfees = querygvw.OrderBy(a => a.overloadkg).Skip(0).Take(1).ToList();
                querygvwdemerit = querygvwdemerit.OrderBy(a => a.category).Where(x => x.category <= GVWOverloadkg );
                var gvwpoint = querygvwdemerit.Skip(0).Take(1).ToList();
                gvwfee= gvwfees[0].fee;
                try
                {
                    gvwpoints_ = gvwpoint[0].point;
                }
                catch (Exception)
                {

                }
                

            }
            //var periodd = period.Substring(5, period.Length);
            if (steeringaxle > 0)
            {
                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                if (steeringaxle > 10000)
                {
                    steeringaxle = 10000;
                }
               // queryavw = queryavw.Where(x => x.period.Substring(5, period.Length) == period.Substring(5, period.Length));

                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                queryavw = queryavw.OrderBy(a => a.overloadkg).Where(x => x.overloadkg >= steeringaxle);
                var steeringaxlevalarr = queryavw.Skip(0).Take(1).ToArray();
                var steeringaxleval = queryavw.Skip(0).Take(1).ToList();
                steeringaxlefee_ = steeringaxleval[0].steeringaxle;
                queryavwdemerit = queryavwdemerit.OrderBy(a => a.category).Where(x => x.category <= steeringaxle);
                var steeringaxlepoints = queryavwdemerit.Skip(0).Take(1).ToList();
                
               
                try
                {
                    steeringaxlepoint_ = steeringaxlepoints[0].point;
                }
                catch (Exception)
                {

                }
            }
           

            if (singledriveaxle > 0)
            {
                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                if (singledriveaxle > 10000)
                {
                    singledriveaxle = 10000;
                }
                //queryavw = queryavw.Where(x => x.period.Substring(0, period.Length) == period);

                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                queryavw = queryavw.OrderBy(a => a.overloadkg).Where(x => x.overloadkg >= singledriveaxle);
                var gvwfees2 = queryavw.ToList();
                var singledriveaxleval = queryavw.Skip(0).Take(1).ToList();
                queryavwdemerit = queryavwdemerit.OrderBy(a => a.category).Where(x => x.category <= singledriveaxle);
                var singledriveaxlepoints = queryavwdemerit.Skip(0).Take(1).ToList();
                singledriveaxlefee_ = singledriveaxleval[0].singledriveaxle;
               
                try
                {
                    singledriveaxlepoint_ = singledriveaxlepoints[0].point;
                }
                catch (Exception)
                {

                }
            }
           
            if (tandemaxle > 0)
            {
                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                if (tandemaxle > 10000)
                {
                    tandemaxle = 10000;
                }
                //queryavw = queryavw.Where(x => x.period.Substring(0, period.Length) == period);

                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                queryavw = queryavw.OrderBy(a => a.overloadkg).Where(x => x.overloadkg >= tandemaxle);
                var tandemaxleval = queryavw.Skip(0).Take(1).ToList();
                queryavwdemerit = queryavwdemerit.OrderBy(a => a.category).Where(x => x.category <= tandemaxle);
                var tandemaxlepoints = queryavwdemerit.Skip(0).Take(1).ToList();
                tandemaxlefee_ = tandemaxleval[0].tandemaxle;
                
                try
                {
                    tandemaxlepoint_ = tandemaxlepoints[0].point;
                }
                catch (Exception)
                {

                }
            }
            

            if (tridemaxle > 0)
            {
                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                if (tridemaxle > 10000)
                {
                    tridemaxle = 10000;
                }
                //queryavw = queryavw.Where(x => x.period.Substring(0, period.Length) == period);

                //var Selections = await _context.GVWoverloadCharges.Where(x => x.vehreg == vehreg).OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
                //return Selections;
                queryavw = queryavw.OrderBy(a => a.overloadkg).Where(x => x.overloadkg >= tridemaxle);
                var tridemaxleval = queryavw.Skip(0).Take(1).ToList();
                queryavwdemerit = queryavwdemerit.OrderBy(a => a.category).Where(x => x.category <= tridemaxle);
                var tridemaxlepoints = queryavwdemerit.Skip(0).Take(1).ToList();
                tridemaxlefee_ = tridemaxleval[0].tridemaxle;
               
                try
                {
                    tridemaxlepoint_ = tridemaxlepoints[0].point;
                }
                catch (Exception)
                {

                }
            }
           

            if (vehreg != null)
            {
                try
                {
                    var vehregpoints = queryvehreg.Where(x => x.vehicle_reg == vehreg).ToList();
                    vehicle_demeritpoints = vehregpoints[0].vehicle_demeritpoints;
                }
                catch(Exception){

                }
                
            }
            totalpoints = vehicle_demeritpoints + tridemaxlepoint_ + tandemaxlepoint_ + singledriveaxlepoint_ + steeringaxlepoint_ + gvwpoints_;
            String penalties = "";
            if (totalpoints > 0)
            {
                var pena = queryPenalties.Where(x => x.points >= totalpoints).Skip(0).Take(1).ToList();
                penalties = pena[0].penalties;
            }
            
            
            var Dollar = queryDollar.OrderByDescending(a => a.id).Skip(0).Take(1).ToList();
            double dollarrate = Dollar[0].rate;
            
            if (post > 0 && caseid != "")
            {
               

            }
            
            var list = new List<EACAct> { new A 
            { gvwfees = gvwfee, 
                gvwpoints = gvwpoints_,
            steeringaxlefee=steeringaxlefee_,
            steeringaxlepoint=steeringaxlepoint_,
            singledriveaxlefee=singledriveaxlefee_,
            singledriveaxlepoint=singledriveaxlepoint_,
            tandemaxlefee=tandemaxlefee_,
            tandemaxlepoint=tandemaxlepoint_,
            tridemaxlefee=tridemaxlefee_,
            vehicle_demeritpoints=vehicle_demeritpoints,
            penalty= penalties,
            dollarrates=dollarrate,
            }
            };

            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            // var Selection = querygvw.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(list);
            return JSONresult;

        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<EACAct>>> GetEACActSearch(String caseid, int offset, int limit =50)
        {
            IQueryable<EACAct> query = _context.EACAct;
            var predicate = PredicateBuilder.False<EACAct>();



            if (caseid != null)
            {
                query = query.Where(x => x.caseid == caseid);
            }



            //var controlSelection = await _context.ControlSelections.FindAsync(roleid);
            var Selection = await query.OrderByDescending(a => a.id).Skip(offset).Take(limit).ToListAsync();
            if (Selection == null)
            {
                return NotFound();
            }
            return Selection;
            
        }


        // POST: api/EACAct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EACAct>> PostEACAct(EACAct EACAct)
        {
            _context.EACAct.Add(EACAct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEACAct", new { id = EACAct.id }, EACAct);
        }

        // DELETE: api/EACAct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEACAct(int id)
        {
            var EACAct = await _context.EACAct.FindAsync(id);
            if (EACAct == null)
            {
                return NotFound();
            }

            _context.EACAct.Remove(EACAct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EACActExists(int id)
        {
            return _context.EACAct.Any(e => e.id == id);
        }
    }

    internal class A : EACAct
    {
        public double gvwfees { get; set; }
        public int gvwpoints { get; set; }
        public double steeringaxlefee { get; set; }
        public int steeringaxlepoint { get; set; }
        public double singledriveaxlefee { get; set; }
        public int singledriveaxlepoint { get; set; }
        public double tandemaxlefee { get; set; }
        public int tandemaxlepoint { get; set; }
        public double tridemaxlefee { get; set; }
        public int tridemaxlepoint { get; set; }
        public int vehicle_demeritpoints { get; set; }
        public String penalty { get; set; }
        public double dollarrates { get; set; }
    }
}
