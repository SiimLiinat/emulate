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
    /// API Controller for GameGenres.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameGenresController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for GameGenres.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public GameGenresController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/GameGenres
        /// <summary>
        /// Get all game genres.
        /// </summary>
        /// <returns>List of GameGenres</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GameGenre>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GameGenre>>> GetGameGenres()
        {
            var gameGenres = await _bll.GameGenres.GetAllAsync();
            return gameGenres.Select(GameGenreMapper.MapToApi).ToList();
        }

        // GET: api/GameGenres/5
        /// <summary>
        /// Get one genre of game. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid id of object to get</param>
        /// <returns>GameGenre entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GameGenre), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameGenre>> GetGameGenre(Guid id)
        {
            var gameGenre = await _bll.GameGenres.FirstOrDefaultAsync(id);
            if (gameGenre == null)
            {
                return NotFound();
            }

            return GameGenreMapper.MapToApi(gameGenre);
        }
        
        // There's no point in editing a many-to-many relationship.
        // // PUT: api/GameGenres/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // /// <summary>
        // /// Put one genre of games into database.
        // /// </summary>
        // /// <param name="id">Guid</param>
        // /// <param name="gameGenre">GameGenre entity</param>
        // /// <returns>No content</returns>
        // [HttpPut("{id:guid}")]
        // public async Task<IActionResult> PutGameGenre(Guid id, GameGenre gameGenre)
        // {
        //     if (id != gameGenre.Id)
        //     {
        //         return BadRequest();
        //     }
        // 
        //     _bll.GameGenres.Update(GameGenreMapper.MapToBll(gameGenre));
        //     await _bll.SaveChangesAsync();
        // 
        //     return NoContent();
        // }

        // POST: api/GameGenres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one genre of games.
        /// </summary>
        /// <param name="gameGenre">GameGenre entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(GameGenre), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GameGenre>> PostGameGenre(GameGenreAdd gameGenre)
        {
            var bllEntity = GameGenreMapper.MapToBll(gameGenre);
            var addedEntity = _bll.GameGenres.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = GameGenreMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetGameGenre", new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, gameGenre);
        }

        // DELETE: api/GameGenres/5
        /// <summary>
        /// Delete one genre of games. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGameGenre(Guid id)
        {
            var gameGenre = await _bll.GameGenres.FirstOrDefaultAsync(id);
            if (gameGenre == null)
            {
                return NotFound();
            }

            _bll.GameGenres.Remove(gameGenre);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
