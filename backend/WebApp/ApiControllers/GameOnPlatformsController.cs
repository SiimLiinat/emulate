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
    /// API Controller for GameOnPlatforms.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameOnPlatformsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for GameOnPlatforms.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public GameOnPlatformsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/GameOnPlatforms
        /// <summary>
        /// Get all games on platforms.
        /// </summary>
        /// <returns>List of GameOnPlatforms</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GameOnPlatform>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GameOnPlatform>>> GetGameOnPlatforms()
        {
            var gameOnPlatforms = await _bll.GameOnPlatforms.GetAllAsync();
            return gameOnPlatforms.Select(GameOnPlatformMapper.MapToApi).ToList();
        }

        // GET: api/GameOnPlatforms/5
        /// <summary>
        /// Get one game on platform. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>GameOnPlatform entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GameOnPlatform), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameOnPlatform>> GetGameOnPlatform(Guid id)
        {
            var gameOnPlatform = await _bll.GameOnPlatforms.FirstOrDefaultAsync(id);
            if (gameOnPlatform == null)
            {
                return NotFound();
            }
            return GameOnPlatformMapper.MapToApi(gameOnPlatform);
        }

        // PUT: api/GameOnPlatforms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one game on platform into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="gameOnPlatform">GameOnPlatform entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutGameOnPlatform(Guid id, GameOnPlatform gameOnPlatform)
        {
            if (id != gameOnPlatform.Id)
            {
                return BadRequest();
            }

            _bll.GameOnPlatforms.Update(GameOnPlatformMapper.MapToBll(gameOnPlatform));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/GameOnPlatforms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one game on platform.
        /// </summary>
        /// <param name="gameOnPlatform">GameOnPlatform entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.APIs.GameOnPlatform), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GameOnPlatform>> PostGameOnPlatform(GameOnPlatformAdd gameOnPlatform)
        {
            var bllEntity = GameOnPlatformMapper.MapToBll(gameOnPlatform);
            var addedEntity = _bll.GameOnPlatforms.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = GameOnPlatformMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetGameOnPlatform", new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, gameOnPlatform);
        }

        // DELETE: api/GameOnPlatforms/5
        /// <summary>
        /// Delete one game on platform. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGameOnPlatform(Guid id)
        {
            var gameOnPlatform = await _bll.GameOnPlatforms.FirstOrDefaultAsync(id);
            if (gameOnPlatform == null)
            {
                return NotFound();
            }

            _bll.GameOnPlatforms.Remove(gameOnPlatform);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
