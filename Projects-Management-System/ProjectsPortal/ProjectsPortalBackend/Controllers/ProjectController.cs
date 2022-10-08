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
    public class ProjectController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ProjectController(ProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProject()
        {
            var projects = await _context.Projects.Select(x => new {

                projectId = x.ProjectId,
                userID = x.User.UserID,
                user = x.User,
                budget = x.Budget,
                title = x.Title,
                description = x.Description,
                isActive = x.IsActive,
                businessUnitID = x.BusinessUnitID,
                businessUnitName = x.BusinessUnit.BusinessUnitName,
                districtName = x.BusinessUnit.District.DistrictName,
                budgetSourceName = x.BusinessUnit.District.BudgetSource.BudgetSourceName

            }).Where(x=>x.isActive==true).ToListAsync();
            return Ok(projects);
        }

        [HttpGet("asset/{userID}")]
        public async Task<ActionResult<User>> GetUserProjects(int userID)
        {
                var projects = await _context.Projects.Select(x => new {

                projectId = x.ProjectId,
                userID = x.User.UserID,
                budget = x.Budget,
                title = x.Title,
                description = x.Description,
                isActive = x.IsActive,
                businessUnitID = x.BusinessUnitID,
                businessUnitName = x.BusinessUnit.BusinessUnitName,
                districtName = x.BusinessUnit.District.DistrictName,
                budgetSourceName = x.BusinessUnit.District.BudgetSource.BudgetSourceName

            }).Where(x=>(x.isActive==true) && userID == x.userID).ToListAsync();
            return Ok(projects);
        }

        [HttpGet("List")]
        public IActionResult List([FromQuery] string searchText, [FromQuery] int? page, [FromQuery] int pagesize=7){

            var query = string.IsNullOrEmpty(searchText)? _context.Projects
                                                        : _context.Projects.Where(x =>
                                                        ((x.Description.ToLower().StartsWith(searchText.ToLower()))||
                                                        (x.Budget.ToString().StartsWith(searchText.ToLower()))||
                                                        (x.Title.ToLower().StartsWith(searchText.ToLower()))));

            int totalCount = query.Count();

            PageResult<Project> result = new PageResult<Project>{

                Count = totalCount,
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = query.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };
            return Ok(result);
        }

        [HttpGet("filter/{filter}")]
        public async Task<IActionResult> SearchProject(string filter)
        {
            filter = filter.ToLower();
            var projects = await _context.Projects.Select(x => new {

                projectId = x.ProjectId,
                userID = x.User.UserID,
                budget = x.Budget,
                title = x.Title,
                description = x.Description,
                isActive = x.IsActive,
                businessUnitID = x.BusinessUnitID,
                businessUnitName = x.BusinessUnit.BusinessUnitName,
                districtName = x.BusinessUnit.District.DistrictName,
                budgetSourceName = x.BusinessUnit.District.BudgetSource.BudgetSourceName

            }).Where(x=>(x.isActive==true)&&
            ((x.description.ToLower().Contains(filter))||
            (x.budget.ToString().Contains(filter))||
            (x.budgetSourceName.ToLower().Contains(filter))||
            (x.districtName.ToLower().Contains(filter))||
            (x.businessUnitName.ToLower().Contains(filter))||
            (x.title.ToLower().Contains(filter)))).ToListAsync();
            return Ok(projects);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if(IsProjectExist(project) || id!=project.ProjectId)
            {
                Helper.createLog(_context, false, 0, "project update", "failed because the asset information is the same");
                await _context.SaveChangesAsync();
                return BadRequest();
            }
            Helper.createLog(_context, true, 0, "project update", "project is updated successfully");
            await _context.SaveChangesAsync();
            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // POST: api/PaymentDetail
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject([FromBody]Project project)
        {
            project.IsActive = true;
            if(!IsProjectExist(project)){
                 _context.Projects.Add(project);
                Helper.createLog(_context, true, 0, "project add", "project is added successfully");
                await _context.SaveChangesAsync();
            }
            else{
                Helper.createLog(_context, false, 0, "project add", "failed because the project information is same with another project");
                await _context.SaveChangesAsync();
                return BadRequest();
            }
            return Ok(project);
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            Helper.createLog(_context, true, 0, "project delete", "project is deleted successfully");
            project.IsActive = false;
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }

        private bool IsProjectExist(Project project) {
            return _context.Projects.Any(e => (e.BusinessUnitID==project.BusinessUnitID
            && e.Title==project.Title
            && e.UserID==project.UserID
            && e.Description==project.Description));
        }
    }
}
