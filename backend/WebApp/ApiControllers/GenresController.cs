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
    /// API Controller for Genres.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenresController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Games.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public GenresController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Genres
        /// <summary>
        /// Get all game/program genres.
        /// </summary>
        /// <returns>List of Genres</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Genre>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            var genres = await _bll.Genres.GetAllAsync();
            return genres.Select(GenreMapper.MapToApi).ToList();
        }

        // GET: api/Genres/5
        /// <summary>
        /// Get one genre. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Genre entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Genre>> GetGenre(Guid id)
        {
            var genre = await _bll.Genres.FirstOrDefaultAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return GenreMapper.MapToApi(genre);
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one genre into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="genre">Genre entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutGenre(Guid id, Genre genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }

            _bll.Genres.Update(GenreMapper.MapToBll(genre));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one genre.
        /// </summary>
        /// <param name="genre">Genre entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Genre), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Genre>> PostGenre(GenreAdd genre)
        {
            var bllEntity = GenreMapper.MapToBll(genre);
            var addedEntity = _bll.Genres.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = GenreMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetGenre", new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, genre);
        }

        // DELETE: api/Genres/5
        /// <summary>
        /// Delete one genre. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            var genre = await _bll.Genres.FirstOrDefaultAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _bll.Genres.Remove(genre);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
