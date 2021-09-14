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
    public class PlatformTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for PlatformTypes.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public PlatformTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PlatformTypes
        /// <summary>
        /// Get all types of platform.
        /// </summary>
        /// <returns>List of PlatformTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PlatformType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PlatformType>>> GetPlatformTypes()
        {
            var platformTypes = await _bll.PlatformTypes.GetAllAsync();
            return platformTypes.Select(PlatformTypeMapper.MapToApi).ToList();
        }

        // GET: api/PlatformTypes/5
        /// <summary>
        /// Get one type of platform. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid id of object to get</param>
        /// <returns>PlatformType entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlatformType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlatformType>> GetPlatformType(Guid id)
        {
            var platformType = await _bll.PlatformTypes.FirstOrDefaultAsync(id);
            if (platformType == null)
            {
                return NotFound();
            }
        
            return PlatformTypeMapper.MapToApi(platformType);
        }

        // PUT: api/PlatformTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one type of platform into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="platformType">PlatformType entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutPlatformType(Guid id, PlatformType platformType)
        {
            if (id != platformType.Id)
            {
                return BadRequest();
            }

            _bll.PlatformTypes.Update(PlatformTypeMapper.MapToBll(platformType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PlatformTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one type of platform.
        /// </summary>
        /// <param name="platformType">PlatformType entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PlatformType), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PlatformType>> PostPlatformType(PlatformTypeAdd platformType)
        {
            var bllEntity = PlatformTypeMapper.MapToBll(platformType);
            var addedEntity = _bll.PlatformTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = PlatformTypeMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetPlatformType", new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, platformType);
        }

        // DELETE: api/PlatformTypes/5
        /// <summary>
        /// Delete one type of platform. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletePlatformType(Guid id)
        {
            var platformType = await _bll.PlatformTypes.FirstOrDefaultAsync(id);
            if (platformType == null)
            {
                return NotFound();
            }
            _bll.PlatformTypes.Remove(platformType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
