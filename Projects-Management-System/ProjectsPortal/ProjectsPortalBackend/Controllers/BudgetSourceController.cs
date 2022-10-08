using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectsPortalBackend.Data;
using ProjectsPortalBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectsPortalBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetSourceController: ControllerBase
    {
        private readonly ProjectContext _context;

        public BudgetSourceController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetSource>>> GetBudgetSource()
        {
            return await _context.BudgetSources.ToListAsync();
        }

    }
}
