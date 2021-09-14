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
    /// API Controller for Progresses.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProgressesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Progresses.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public ProgressesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Progresses
        /// <summary>
        /// Get all progresses.
        /// </summary>
        /// <returns>List of Progresses</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Progress>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Progress>>> GetProgresses(Guid? userId, Guid? gameId)
        {
            var progresses = await _bll.Progresses.GetAllProgresses(userId, gameId);
            return progresses.Select(ProgressMapper.MapToApi).ToList();
        }

        // GET: api/Progresses/5
        /// <summary>
        /// Get one progress. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid id of object to get</param>
        /// <returns>Progress entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Progress), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<Progress>> GetProgress(Guid id)
        {
            var progress = await _bll.Progresses.FirstOrDefaultAsync(id);

            if (progress == null)
            {
                return NotFound();
            }

            return ProgressMapper.MapToApi(progress);
        }

        // PUT: api/Progresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one progress into database.
        /// </summary>
        /// <param name="id">Guid id of object to put</param>
        /// <param name="progress">Progress entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutProgress(Guid id, Progress progress)
        {
            if (id != progress.Id)
            {
                return BadRequest();
            }

            var bllProgress = ProgressMapper.MapToBll(progress);
            bllProgress.EditedAt = DateTime.Now;
            _bll.Progresses.Update(bllProgress);

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Progresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one progress.
        /// </summary>
        /// <param name="progress">Progress entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Progress), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Progress>> PostProgress(ProgressAdd progress)
        {
            var bllEntity = ProgressMapper.MapToBll(progress);
            var addedEntity = _bll.Progresses.Add(bllEntity);
            await _bll.SaveChangesAsync();
            addedEntity = await _bll.Progresses.FirstOrDefaultAsync(addedEntity.Id);
            var returnEntity = ProgressMapper.MapToApi(addedEntity!);

            return CreatedAtAction("GetProgress", new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, progress);
        }

        // DELETE: api/Progresses/5
        /// <summary>
        /// Delete one progress. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid id of object to delete</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteProgress(Guid id)
        {
            var progress = await _bll.Progresses.FirstOrDefaultAsync(id);
            if (progress == null)
            {
                return NotFound();
            }

            _bll.Progresses.Remove(progress);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
