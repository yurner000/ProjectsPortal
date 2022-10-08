using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectsPortalBackend.Data;
using ProjectsPortalBackend.DTOS;
using ProjectsPortalBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectsPortalBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogOperationsController: ControllerBase
    {
        private readonly ProjectContext _context;

        public LogOperationsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _context.LogOperations.OrderByDescending(x=>x.DateTime).ToListAsync();
            return Ok(logs);
        }

        [HttpGet("filter/{filter}")]
        public async Task<IActionResult> SearchLogs(string filter)
        {
            filter = filter.ToLower();
            var logs = new List<LogOperations>();
            logs = await _context.LogOperations
            .Where(x=>
            (x.Process.ToLower().Contains(filter))||
            (x.Statement.ToLower().Contains(filter))||
            (x.DateTime.ToString().Contains(filter))||
            (x.UserIp.ToLower().Contains(filter))).ToListAsync();
            return Ok(logs);
        }

        [HttpGet("List")]
        public IActionResult List([FromQuery] string searchText, [FromQuery] int? page, [FromQuery] int pagesize=7){

            var query = string.IsNullOrEmpty(searchText)? _context.LogOperations
                                                        : _context.LogOperations.Where(e =>
                                                                        e.Statement.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.Process.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.UserIp.ToLower().Contains(searchText.ToLower()));

            int totalCount = query.Count();

            PageResult<LogOperations> result = new PageResult<LogOperations>{

                Count = totalCount,
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = query.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };
            return Ok(result);
        }

    }
}
