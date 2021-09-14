using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.APIs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for Platforms.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlatformsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Platforms.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public PlatformsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Platforms
        /// <summary>
        /// Get all platforms.
        /// </summary>
        /// <returns>List of Platforms</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Platform>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Platform>>> GetPlatforms()
        {
            var platforms = await _bll.Platforms.GetAllAsync();
            return platforms.Select(PlatformMapper.MapToApi).ToList();
        }

        // GET: api/Platforms/5
        /// <summary>
        /// Get one platform. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Platform entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Platform), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Platform>> GetPlatform(Guid id)
        {
            var platform = await _bll.Platforms.FirstOrDefaultAsync(id);

            if (platform == null)
            {
                return NotFound();
            }

            return PlatformMapper.MapToApi(platform);
        }

        // PUT: api/Platforms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one platform into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="platform">Platform entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutPlatform(Guid id, Platform platform)
        {
            if (id != platform.Id)
            {
                return BadRequest();
            }

            _bll.Platforms.Update(PlatformMapper.MapToBll(platform));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Platforms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one platform.
        /// </summary>
        /// <param name="platform">Platform entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.APIs.Platform), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Platform>> PostPlatform(PlatformAdd platform)
        {
            var bllEntity = PlatformMapper.MapToBll(platform);
            var addedEntity = _bll.Platforms.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = PlatformMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetPlatform", new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, platform);
        }

        // DELETE: api/Platforms/5
        /// <summary>
        /// Delete one platform. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletePlatform(Guid id)
        {
            var platform = await _bll.Platforms.FirstOrDefaultAsync(id);
            if (platform == null)
            {
                return NotFound();
            }

            _bll.Platforms.Remove(platform);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
