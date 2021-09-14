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
    /// API Controller for MediaTypes.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MediaTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for MediaTypes.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public MediaTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/MediaTypes
        /// <summary>
        /// Get all types of media.
        /// </summary>
        /// <returns>List of MediaTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MediaType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<MediaType>>> GetMediaTypes()
        {
            var mediaTypes = await _bll.MediaTypes.GetAllAsync();
            return mediaTypes.Select(MediaTypeMapper.MapToApi).ToList();
        }

        // GET: api/MediaTypes/5
        /// <summary>
        /// Get one type of media. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid id of object to get</param>
        /// <returns>MediaType entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MediaType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MediaType>> GetMediaType(Guid id)
        {
            var mediaType = await _bll.MediaTypes.FirstOrDefaultAsync(id);
            if (mediaType == null)
            {
                return NotFound();
            }
            return MediaTypeMapper.MapToApi(mediaType);
        }

        // PUT: api/MediaTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one type of media into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="mediaType">MediaType entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutMediaType(Guid id, MediaType mediaType)
        {
            if (id != mediaType.Id)
            {
                return BadRequest();
            }

            _bll.MediaTypes.Update(MediaTypeMapper.MapToBll(mediaType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/MediaTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one type of media.
        /// </summary>
        /// <param name="mediaType">MediaType entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(MediaType), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<MediaType>> PostMediaType(MediaTypeAdd mediaType)
        {
            var bllEntity = MediaTypeMapper.MapToBll(mediaType);
            var addedEntity = _bll.MediaTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = MediaTypeMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetMediaType", new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, mediaType);
        }

        // DELETE: api/MediaTypes/5
        /// <summary>
        /// Delete one type of media. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteMediaType(Guid id)
        {
            var mediaType = await _bll.MediaTypes.FirstOrDefaultAsync(id);
            if (mediaType == null)
            {
                return NotFound();
            }

            _bll.MediaTypes.Remove(mediaType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
