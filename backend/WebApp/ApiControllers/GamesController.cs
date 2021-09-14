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
    /// API Controller for Games.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Games.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public GamesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Games
        /// <summary>
        /// Get all games and/or programs.
        /// </summary>
        /// <returns>List of Games</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Game>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            var games = await _bll.Games.GetAllGames();
            return games.Select(GameMapper.MapToApi).ToList();
        }

        // GET: api/Games/5
        /// <summary>
        /// Get one game. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Game entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<Game>> GetGame(Guid id)
        {
            var game = await _bll.Games.GetGame(id);
            if (game == null)
            {
                return NotFound();
            }
            return GameMapper.MapToApi(game);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one game into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="game">Game entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutGame(Guid id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }
            _bll.Games.Update(GameMapper.MapToBll(game));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one game.
        /// </summary>
        /// <param name="game">Game entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Game>> PostGame(GameAdd game)
        {
            var bllEntity = GameMapper.MapToBll(game);
            var addedEntity = _bll.Games.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = GameMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetGame", new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, game);
        }

        // DELETE: api/Games/5
        /// <summary>
        /// Delete one game. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var game = await _bll.Games.FirstOrDefaultAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _bll.Games.Remove(game);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
