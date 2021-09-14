using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1.APIs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for AppUsers.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppUsersController : ControllerBase
    {
        private UserManager<Domain.App.Identity.AppUser> _userManager;

        /// <summary>
        /// API Controller for AppUsers.
        /// </summary>
        /// <param name="userManager">User Manager</param>
        public AppUsersController(UserManager<Domain.App.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/AppUsers
        /// <summary>
        /// Get all AppUsers.
        /// </summary>
        /// <returns>List of AppRoles</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<AppUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<IEnumerable<AppUser>> GetAppUsers()
        {
            var appUsers = _userManager.Users.ToList();
            return appUsers.Select(AppUserMapper.MapToApi).ToList();
        }

        // GET: api/AppUsers/5
        /// <summary>
        /// Get one AppUser. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>AppRole entity from database</returns>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AppUser), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppUser>> GetAppUser(Guid id)
        {
            var appUser = await _userManager.FindByIdAsync(id.ToString());

            if (appUser == null)
            {
                return NotFound();
            }

            return AppUserMapper.MapToApi(appUser);
        }

        // PUT: api/AppUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one AppUser into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="appUser">AppUser entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutAppUser(Guid id, AppUser appUser)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
            {
                return BadRequest();
            }

            user.Email = appUser.Email;
            user.ProfilePicture = appUser.ProfilePicture;
            user.LockoutEnd = appUser.LockoutEnd;
            await _userManager.UpdateAsync(user);
            return NoContent();
        }

        // // POST: api/AppUsers
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // /// <summary>
        // /// Post one user.
        // /// </summary>
        // /// <param name="appUser">AppUser entity</param>
        // /// <returns>CreatedAtAction</returns>
        // [HttpPost]
        // [Consumes("application/json")]
        // [ProducesResponseType(typeof(AppUser), StatusCodes.Status201Created)]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
        // {
        //     var bllEntity = appUser;
        //     var addedEntity = _bll.AppUsers.Add(bllEntity);
        //     await _bll.SaveChangesAsync();
        //     var returnEntity = addedEntity;
        // 
        //     return CreatedAtAction("GetAppUser", new { id = returnEntity.Id,
        //         version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, appUser);
        // }

        // DELETE: api/AppUsers/5
        /// <summary>
        /// Delete one user. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAppUser(Guid id)
        {
            var appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(appUser);

            return NoContent();
        }
    }
}
