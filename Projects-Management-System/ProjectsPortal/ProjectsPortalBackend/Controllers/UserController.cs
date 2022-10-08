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
    public class UserController : ControllerBase
    {
        private readonly ProjectContext _context;

        public UserController(ProjectContext context)
        {
            _context = context;
        }

        [HttpGet("filter/{filter}")]
        public async Task<IActionResult> SearchUser(string filter)
        {
            filter = filter.ToLower();
            var user = new List<User>();
            user = await _context.Users
            .Where(x=>(x.IsActive==true)&&
            ((x.UserFirstname.ToLower().Contains(filter))||
            (x.UserSecondname.ToLower().Contains(filter))||
            (x.UserDepartment.ToLower().Contains(filter))||
            (x.UserEmail.ToLower().Contains(filter)))).ToListAsync();
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _context.Users.Select(x => new {

                userID = x.UserID,
                userName = x.UserFirstname,
                userSecondname = x.UserSecondname,
                userEmail = x.UserEmail,
                userRole = x.UserRole,
                isActive = x.IsActive,
                userDepartment = x.UserDepartment,
                userPassword = DTOS.Helper.DecodeFrom64(x.UserPassword)

            }).Where(x=>x.isActive==true).ToListAsync();
            return Ok(users);
        }

        [HttpGet("List")]
        public IActionResult List([FromQuery] string searchText, [FromQuery] int? page, [FromQuery] int pagesize=7){

            var query = string.IsNullOrEmpty(searchText)? _context.Users
                                                        : _context.Users.Where(e =>
                                                                        e.UserFirstname.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.UserSecondname.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.UserEmail.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.UserDepartment.ToLower().Contains(searchText.ToLower()));

            int totalCount = query.Count();

            PageResult<User> result = new PageResult<User>{

                Count = totalCount,
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = query.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if(isUserExist(user) || id!=user.UserID)
            {
                Helper.createLog(_context, false, 0, "user update", "failed because the user information is the same");
                await _context.SaveChangesAsync();
                return BadRequest();
            }
            user.UserPassword = Helper.EncodePasswordToBase64(user.UserPassword);
            _context.Entry(user).State = EntityState.Modified;

            Helper.createLog(_context, true, 0, "user update", "user is updated successfully");
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("login/{id}")]
        public async Task<ActionResult<User>> LOginUser(int id)
        {
            Helper.createLog(_context, true, id, "login", "user is logged in");
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("logout/{id}")]
        public async Task<IActionResult> LogOutUser(int id)
        {
            Helper.createLog(_context, true, id, "logout", "user is logged out");
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.IsActive = true;
            if(!isUserExist(user)){
                _context.Users.Add(user);
                Helper.createLog(_context, true, 0, "user add", "user is added successfully");
                user.UserPassword = Helper.EncodePasswordToBase64(user.UserPassword);
                await _context.SaveChangesAsync();
            }
            else{
                Helper.createLog(_context, false, 0, "user add", "failed because the user information is same with another user");
                await _context.SaveChangesAsync();
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            Helper.createLog(_context, true, 0, "user delete", "user is deleted");
            user.IsActive = false;
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        private bool isUserExist(User user) {
            return _context.Users.Any(e => (e.UserFirstname==user.UserFirstname
            && e.UserSecondname==user.UserSecondname
            && e.UserEmail==user.UserEmail
            && e.UserDepartment==user.UserDepartment
            && e.UserRole==user.UserRole));
        }

    }
}
