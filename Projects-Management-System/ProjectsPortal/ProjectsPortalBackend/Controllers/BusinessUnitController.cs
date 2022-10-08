using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectsPortalBackend.Data;
using ProjectsPortalBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectsPortalBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessUnitController: ControllerBase
    {
        private readonly ProjectContext _context;

        public BusinessUnitController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<IActionResult> GetBusinessUnit()
        {
            var businessUnits = await _context.BusinessUnits.ToListAsync();
            return Ok(businessUnits);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessUnit>> GetBusinessUnit(int id)
        {
            var businessUnits = await _context.BusinessUnits.FindAsync(id);

            if (businessUnits == null)
            {
                return NotFound();
            }

            return Ok(businessUnits);
        }

        [HttpGet("filter/{district_id}")]
        public async Task<IActionResult> SearchBusinessUnit(int districtID)
        {
            var businessUnit = await _context.BusinessUnits
            .Where(x=>x.DistrictID==districtID).ToListAsync();
            return Ok(businessUnit);
        }
    }
}
