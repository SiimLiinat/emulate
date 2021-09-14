using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1.APIs;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for AppUserRoles.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppUserRolesController : ControllerBase
    {
        private RoleManager<Domain.App.Identity.AppRole> _roleManager;
        private UserManager<Domain.App.Identity.AppUser> _userManager;

        /// <summary>
        /// API Controller for AppUsers.
        /// </summary>
        /// <param name="roleManager">Role Manager</param>
        /// <param name="userManager">User Manager</param>
        public AppUserRolesController(RoleManager<Domain.App.Identity.AppRole> roleManager, UserManager<Domain.App.Identity.AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: api/AppUserRoles
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<AppUserRole>>> GetAppUserRoles()
        // {
        //     return _context.AppUserRoles.ToList();
        // }
         
        // GET: api/AppUserRoles/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<AppUserRole>> GetAppUserRole(Guid id)
        // {
        //     var appUserRole = await _context.AppUserRoles.FindAsync(id);
        //  
        //     if (appUserRole == null)
        //     {
        //         return NotFound();
        //     }
        //  
        //     return appUserRole;
        // }

        /*// PUT: api/AppUserRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUserRole(Guid id, AppUserRole appUserRole)
        {
            if (id != appUserRole.UserId)
            {
                return BadRequest();
            }
         
            _context.Entry(appUserRole).State = EntityState.Modified;
         
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserRoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
         
            return NoContent();
        }*/

        // POST: api/AppUserRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Creates one userRole.
        /// </summary>
        /// <param name="appUserRole">AppUserRole containing UserId and RoleId</param>
        /// <returns>NoContent</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> PostAppUserRole(AppUserRole appUserRole)
        {
            var user = await _userManager.FindByIdAsync(appUserRole.UserId.ToString());
            var role = await _roleManager.FindByIdAsync(appUserRole.RoleId.ToString());
        
            if (user == null || role == null) return BadRequest();
            await _userManager.AddToRoleAsync(user, role.Name);
        
            return NoContent();
        }
        
        // DELETE: api/AppUserRoles
        /// <summary>
        /// Delete one userRole.
        /// </summary>
        /// <returns>No content.</returns>
        [HttpDelete]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAppUserRole(AppUserRole appUserRole)
        {
            var user = await _userManager.FindByIdAsync(appUserRole.UserId.ToString());
            var role = await _roleManager.FindByIdAsync(appUserRole.RoleId.ToString());
        
            if (user == null || role == null) return BadRequest();
            await _userManager.RemoveFromRoleAsync(user, role.Name);
        
            return NoContent();
        }
        
    }
}