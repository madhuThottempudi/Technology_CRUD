using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnologyApplication.Migrations;
using TechnologyApplication.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TechnologyApplication.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        [HttpPost]
        [Route("list")]
        public async Task<ActionResult<List<Company>>> PostAddCompany([FromBody]List<Company> compan)
        {
            using (var comp = new TechnologyContext())
            {
                foreach (var item in compan)
                {
                    comp.Companies.Add( new Company {Id = item.Id, Com_Name = item.Com_Name, Comp_Type = item.Comp_Type, OwnerName = item.OwnerName, Comp_est = item.Comp_est });
                    comp.SaveChanges();
                }
                return Ok(compan);
            }
            
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            using (var comp = new TechnologyContext())
            {
                IList<Company> companies = new List<Company>();
                 companies = comp.Companies.Where(x => x.Id == id).ToList();
                //companies = comp.Companies.Include("CompanyProfit").Select(s => new Company() { Id = s.Id,} ).FirstOrDefault();
                if (companies.Count == 0)
                {
                    return BadRequest("No Content Found");
                }
                return Ok(companies);
            }

        }
        [HttpPut]
        public async Task<ActionResult<List<Company>>> PutUpdateCompany(Company mnccompany)
        {
            using (var comp = new TechnologyContext())
            {
                var existingCompany = comp.Companies.Where(c =>c.Id== mnccompany.Id).FirstOrDefault();
                if (existingCompany != null)
                {
                  
                    existingCompany.Com_Name = mnccompany.Com_Name;
                    existingCompany.Comp_Type = mnccompany.Comp_Type;
                    existingCompany.OwnerName = mnccompany.OwnerName;
                    existingCompany.Comp_est = mnccompany.Comp_est;
                    comp.SaveChanges();
                   
                }
                else
                {
                    return BadRequest("no raecords on that Id");
                }
                return Ok();
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            using (var comp = new TechnologyContext())
            {
                var compas = comp.Companies.Where(c => c.Id == id).FirstOrDefault();
                comp.Remove(compas);
                comp.SaveChanges();
                //comp.Remove(compas);
                //comp.SaveChanges();
                //comp.Companies.Remove(TechnologyContext.Companies.FirstOrDefault(e => e.Id == id));
                //comp.SaveChanges();
            }
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<Company>> fjhygj( int id)
        {
            return StatusCode(StatusCodes.Status200OK,"");
        }

    }
}
