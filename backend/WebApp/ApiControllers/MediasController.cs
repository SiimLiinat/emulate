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
using MediaMapper = PublicApi.DTO.v1.Mappers.MediaMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for Medias.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MediasController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Medias.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public MediasController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Medias
        /// <summary>
        /// Get all medias.
        /// </summary>
        /// <returns>List of Medias</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Media>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Media>>> GetMedias(Guid? userId, Guid? gameId)
        {
            var medias= await _bll.Medias.GetAllMedias(userId, gameId);
            return medias.Select(MediaMapper.MapToApi).ToList();
        }

        // GET: api/Medias/5
        /// <summary>
        /// Get one media. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid id of object to get</param>
        /// <returns>Media entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Media), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<Media>> GetMedia(Guid id)
        {
            var media = await _bll.Medias.FirstOrDefaultAsync(id);
            if (media == null)
            {
                return NotFound();
            }
            return MediaMapper.MapToApi(media);
        }

        // PUT: api/Medias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one media into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="media">Media entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutMedia(Guid id, Media media)
        {
            if (id != media.Id)
            {
                return BadRequest();
            }

            _bll.Medias.Update(MediaMapper.MapToBll(media));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Medias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one media.
        /// </summary>
        /// <param name="media">Media entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Media), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Media>> PostMedia(MediaAdd media)
        {
            var bllEntity = MediaMapper.MapToBll(media);
            var addedEntity = _bll.Medias.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = MediaMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetMedia", new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, media);
        }

        // DELETE: api/Medias/5
        /// <summary>
        /// Delete one media. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteMedia(Guid id)
        {
            var media = await _bll.Medias.FirstOrDefaultAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            _bll.Medias.Remove(media);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
