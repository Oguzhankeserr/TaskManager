using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.CommonModels;
using TaskManager.Identity.Domain.Entities;
using TaskManager.Identity.Infrastructure.Context;

namespace TaskManager.Identity.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly TaskManagerDbContext _taskManagerDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(TaskManagerDbContext taskManagerDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _taskManagerDbContext = taskManagerDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<ActionResponse<AppRole>> CreateRole(string name)
        {
            ActionResponse<AppRole> response = new();
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (roleExist != null)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));
               
                if (roleResult.Succeeded)
                {
                    response.Message = "Role successfully created";
                }
                else
                {
                    response.Message = "Role does not created";
                }
            }
            return response;
        }
    }
}
