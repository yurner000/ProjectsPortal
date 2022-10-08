using System.Linq;
using System.Threading.Tasks;
using ProjectsPortalBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectsPortalBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistrictController: ControllerBase
    {
        private readonly ProjectContext _context;

        public DistrictController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<IActionResult> GetDistrict()
        {
            var district = await _context.Districts.ToListAsync();
            return Ok(district);
        }

        [HttpGet("filter/{budgetSource_id}")]
        public async Task<IActionResult> SearchDistrict(int budgetSourceID)
        {
            var district = await _context.Districts
            .Where(x=>x.BudgetSourceID==budgetSourceID).ToListAsync();
            return Ok(district);
        }
    }
}
