using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1.APIs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for AppRoles.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppRolesController : ControllerBase
    {
        private RoleManager<Domain.App.Identity.AppRole> _roleManager;

        /// <summary>
        /// API Controller for AppRoles.
        /// </summary>
        /// <param name="roleManager">Role Manager</param>
        public AppRolesController(RoleManager<Domain.App.Identity.AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: api/AppRoles
        /// <summary>
        /// Get all AppRoles.
        /// </summary>
        /// <returns>List of AppRoles</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<AppRole>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<IEnumerable<AppRole>> GetAppRoles()
        {
            var appRoles = _roleManager.Roles.ToList();
            return appRoles.Select(AppRoleMapper.MapToApi).ToList();
        }

        // GET: api/AppRoles/5
        /// <summary>
        /// Get one role. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Company entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AppRole), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppRole>> GetAppRole(Guid id)
        {
            var appRole = await _roleManager.FindByIdAsync(id.ToString());

            if (appRole == null)
            {
                return NotFound();
            }

            return AppRoleMapper.MapToApi(appRole);
        }

        // PUT: api/AppRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one AppRole into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="appRole">AppRole entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutAppRole(Guid id, AppRoleAdd appRole)
        {
            var role = _roleManager.FindByIdAsync(id.ToString()).Result;
            if (role == null)
            {
                return BadRequest();
            }
            role.Name = appRole.Name;
            role.NormalizedName = appRole.Name.Normalize();
            await _roleManager.UpdateAsync(role);
            return NoContent();
        }

        // POST: api/AppRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one appRole.
        /// </summary>
        /// <param name="appRole">AppRole entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(AppRole), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AppRole>> PostAppRole(AppRoleAdd appRole)
        {
            var role = _roleManager.FindByNameAsync(appRole.Name).Result;
            if (role == null)
            {
                role = new Domain.App.Identity.AppRole
                {
                    Name = appRole.Name,
                    NormalizedName = appRole.Name.Normalize()
                };
                
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    
                }
            }
            return CreatedAtAction("GetAppRole", new { id = _roleManager.FindByNameAsync(appRole.Name), 
                version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, appRole);
        }

        // DELETE: api/AppRoles/5
        /// <summary>
        /// Delete one role. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAppRole(Guid id)
        {
            var appRole = await _roleManager.FindByIdAsync(id.ToString());
            if (appRole == null)
            {
                return NotFound();
            }
            await _roleManager.DeleteAsync(appRole);

            return NoContent();
        }
    }
}
